using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerTools
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int arrowCount = 4;
        public int ArrowCount
        {
            get { return arrowCount; }
            set
            {
                if (value > 0)
                    arrowCount = value;
            }
        }
        [SerializeField] private float speed = 5.0f;
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value > 0.1)
                    speed = value;
            }
        }
        [SerializeField] private float jumpforce = 3.0f;
        public float JumpForce
        {
            get { return jumpforce; }
            set
            {
                if (value > 0.1f && value < 8f)
                    jumpforce = value;
            }
        }
        public float shootCoeff = 3.0f;
        public int minimalHeight = -5;
        public bool shoot;
        [SerializeField] private bool isCheatMode;
        public SpriteRenderer[] renderers = { };
        [SerializeField] private Rigidbody2D playerbody;
        [SerializeField] private GroundDetection groundDetection;
        private Vector3 direction;
        private bool isJumping;
        private List<GameObject> arrowPool;
        [SerializeField] private Animator anima;
        [SerializeField] private SpriteRenderer playerRender;
        [SerializeField] private GameObject arrow;
        [SerializeField] private Transform arrowSpawnPoint;
        [SerializeField] public BuffReciever buffReceiver;

        public float bonusForce;
        public float bonusDamage;
        public float bonusArmor;
        private Buff forceBuff, damageBuff, armorBuff;
        private Health health;
        [SerializeField] private Text healthText;

        void Awake()
        {
            Instance = this;
        }

        public static Player Instance { get; set; }

        // Initialization
        void Start()
        {
            arrowPool = new List<GameObject>();
            for (int i = 0; i < arrowCount; i++)
            {
                var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
                arrowPool.Add(arrowTemp);
                arrowTemp.gameObject.SetActive(false);
                GameManager.Instance.animatorContainer.Add(arrowTemp.gameObject, arrowTemp.gameObject.GetComponent<Animator>());
            }

            GameManager.Instance.animatorContainer.Add(gameObject, gameObject.GetComponent<Animator>());

            buffReceiver.OnBuffsChanged += ApplyBuff;

            health = GameManager.Instance.healthContainer[this.gameObject];
            healthText.text = " "+health.health;

            // Changing color
            for (int j = 0; j < renderers.Length; j++)
                renderers[j].color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        }

        private void ApplyBuff()
        {
            forceBuff = buffReceiver.Buffs.Find(t => t.type == BuffType.Force);
            damageBuff = buffReceiver.Buffs.Find(t => t.type == BuffType.Damage);
            armorBuff = buffReceiver.Buffs.Find(t => t.type == BuffType.Armor);
            bonusForce = forceBuff == null ? 0 : forceBuff.additiveBonus;
            bonusArmor = armorBuff == null ? 0 : armorBuff.additiveBonus;
            if (health.maximumHealth != health.typicalHealth + (int)bonusArmor)
                health.GiveHealth((int)bonusArmor);
            health.SetMaximumHealth(health.typicalHealth + (int)bonusArmor);
            bonusDamage = damageBuff == null ? 0 : damageBuff.additiveBonus;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            anima.SetBool("isGrounded", groundDetection.isGrounded);
            isJumping = isJumping && !groundDetection.isGrounded;
            direction = Vector3.zero;
            if (!isJumping && !groundDetection.isGrounded)
                anima.SetTrigger("StartFall");
            if (Input.GetKey(KeyCode.A))
                //transform.Translate(Vector2.left*Time.deltaTime*speed);
                direction = Vector3.left;
            if (Input.GetKeyDown(KeyCode.Space) && groundDetection.isGrounded)
            {
                playerbody.AddForce(Vector2.up * (jumpforce + bonusForce), ForceMode2D.Impulse);
                anima.SetTrigger("StartJump");
                isJumping = true;
            }
            if (Input.GetKey(KeyCode.D))
                direction = Vector3.right;
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            direction *= speed;
            direction.y = playerbody.velocity.y;
            playerbody.velocity = direction;
            if (direction.x > 0)
            {
                playerRender.flipX = false;
                arrowSpawnPoint.transform.localPosition = new Vector3(0.341f, 0.185f, 0);
            }
            if (direction.x < 0)
            {
                playerRender.flipX = true;
                arrowSpawnPoint.transform.localPosition = new Vector3(-0.341f, 0.185f, 0);
            }
            anima.SetFloat("Speed", Mathf.Abs(direction.x));
            CheckFall();
        }

        private void Update()
        {
            CheckShoot();

            healthText.text = "" + health.health;
        }

        void CheckShoot()
        {
            if (Input.GetMouseButtonDown(0) && shoot == false && groundDetection.isGrounded)
            {
                anima.SetBool("Shoot", true);
                shoot = true;
                StartCoroutine(StartShoot());
            }
        }
        
        private IEnumerator StartShoot()
        {
            yield return new WaitWhile(() => shoot == true);
            GameObject cArrow = GetArrowFromPool();
            cArrow.GetComponent<Arrow>().SetImpulse(Vector2.right, playerRender.flipX ? -jumpforce * shootCoeff : jumpforce * shootCoeff, (int)bonusDamage, this);
            anima.SetBool("Shoot", false);
            yield break;
        }

        public void Shooting()
        {
            shoot = false;
        }

        private GameObject GetArrowFromPool()
        {
            if (arrowPool.Count > 0)
            {
                var arrowTemp = arrowPool[0];
                arrowPool.Remove(arrowTemp);
                arrowTemp.gameObject.SetActive(true);
                arrowTemp.transform.parent = null;
                arrowTemp.transform.position = arrowSpawnPoint.transform.position;
                arrowTemp.transform.rotation = Quaternion.identity;
                return arrowTemp;
            }
            return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity); // Запасное создание стрелы
        }

        public void ReturnArrowToPool(GameObject arrowTemp)
        {
            if (!arrowPool.Contains(arrowTemp))
                arrowPool.Add(arrowTemp);

            arrowTemp.transform.parent = arrowSpawnPoint;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            arrowTemp.gameObject.SetActive(false);
        }

        // Processing of falling
        void CheckFall()
        {
            if (transform.position.y < minimalHeight && isCheatMode)
            {
                playerbody.velocity = new Vector2(0, 0);
                transform.position = new Vector2(0, 0);
            }
            else if (transform.position.y < minimalHeight)
                Destroy(gameObject);
        }
    }
}

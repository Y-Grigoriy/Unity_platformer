using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerTools
{
    public class Player : MonoBehaviour
    {

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
        public float jumpforce = 4.0f;
        public float shootCoeff = 3.0f;
        public int minimalHeight = -5;
        public bool shoot;
        public bool isCheatMode;
        public SpriteRenderer[] renderers = { };
        [SerializeField] private  Rigidbody2D playerbody;
        public GroundDetection groundDetection;
        private Vector3 direction;
        [SerializeField] private  Animator anima;
        [SerializeField] private  SpriteRenderer playerRender;
        private bool isJumping;
        [SerializeField] private GameObject arrow;
        [SerializeField] private Transform arrowSpawnPoint;
                
        void Awake()
        {
            Instance = this;
        }

        public static Player Instance { get; set; }

        // Initialization
        void Start()
        {
            // Changing color
            for (int j = 0; j < renderers.Length; j++)
                renderers[j].color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
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
                playerbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
                anima.SetTrigger("StartJump");
                isJumping = true;
            }
            if (Input.GetKey(KeyCode.D))
                //transform.Translate(Vector2.right * Time.deltaTime * speed);
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
        }

        void CheckShoot()
        {
            if (Input.GetMouseButtonDown(0) && shoot == false)
            {
                gameObject.GetComponent<Animator>().SetBool("Shoot", true);
                shoot = true;
                StartCoroutine(StartShoot());
            }
        }
        
        private IEnumerator StartShoot()
        {
            yield return new WaitWhile(() => shoot == true);
            GameObject prefab = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
            prefab.GetComponent<Arrow>().SetImpulse(Vector2.right, playerRender.flipX ? -jumpforce * shootCoeff : jumpforce * 2.0f, gameObject);
            gameObject.GetComponent<Animator>().SetBool("Shoot", false);
            yield break;
        }

        public void Shooting()
        {
            shoot = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Gold"))
            {
                PlayerInventory.Instance.goldCount++;
                Debug.Log("Amount of gold" + PlayerInventory.Instance.goldCount);
                Destroy(col.gameObject);
            }

            if (col.gameObject.CompareTag("Gems"))
            {
                PlayerInventory.Instance.gemsCount++;
                Debug.Log("Amount of gems" + PlayerInventory.Instance.gemsCount);
                Destroy(col.gameObject);
            }

            if (col.gameObject.CompareTag("Kristall"))
            {
                PlayerInventory.Instance.kristallsCount++;
                Debug.Log("Amount of kristalls" + PlayerInventory.Instance.kristallsCount);
                Destroy(col.gameObject);
            }
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

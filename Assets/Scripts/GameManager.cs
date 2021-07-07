using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static GameManager Instance { get; private set; }

    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Animator> animatorContainer;
    public Dictionary<GameObject, Resource> resourcesContainer;
    public Dictionary<GameObject, BuffReciever> buffReceiverContainer;
    public Dictionary<GameObject, ItemComponent> itemsContainer;
    [HideInInspector] public PlayerInventory inventory;
    public ItemBase itemDataBase;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private Image soundButtonRender;
    [SerializeField] private Sprite[] sbuttonRenders = new Sprite[2];
    [SerializeField] private int soundSwitch=1;

    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        animatorContainer = new Dictionary<GameObject, Animator>();
        resourcesContainer = new Dictionary<GameObject, Resource>();
        buffReceiverContainer = new Dictionary<GameObject, BuffReciever>();
        itemsContainer = new Dictionary<GameObject, ItemComponent>();
        menuPause.SetActive(false);

        if (PlayerPrefs.HasKey("Sound"))
        {
            soundSwitch = PlayerPrefs.GetInt("Sound");
            soundButtonRender.sprite = soundSwitch == 1 ? sbuttonRenders[0] : sbuttonRenders[1];
        } 
    }
    

    public void OnClickPause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            menuPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menuPause.SetActive(false);
        }
    }

    public void OnClickInventory()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            inventoryPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            inventoryPanel.SetActive(false);
        }
    }

    public void OnClickContinue()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickMenu()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(0);
    }

    public void OnClickSound()
    {
        if (soundSwitch==1)
        {
            soundSwitch = 0;
            soundButtonRender.sprite = sbuttonRenders[1];
        }
        else
        {
            soundSwitch = 1;
            soundButtonRender.sprite = sbuttonRenders[0];
        }
        PlayerPrefs.SetInt("Sound", soundSwitch);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
    /*private void Start () {
        var healthObjects = FindObjectsOfType<Health>();
        foreach (var health in healthObjects)
        {
            healthContainer.Add(health.gameObject, health);
        }
	}*/
}

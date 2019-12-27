using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MainUi : MonoBehaviour
{
    private static MainUi _instance;
    public static MainUi Instance;



    public GameObject MainUiPannel;
    public bool MainPannelIsOpen;
    public GameObject MainInventoryPannel;
    public bool InventoryLoaded;
    public GameObject ItemButton;
    public GameObject ItemParrent;
    public GameObject InformationPannel;
    public bool MainInventoryIsOpen;
    [Header("Quest settings")]
     public GameObject MainQuestPannel;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        MainUiPannel.SetActive(false);
        MainInventoryPannel.SetActive(false);
        MainQuestPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForOpenUI();
        Checkforopen();
    }

  
    public void Checkforopen()
    {       
        if (Input.GetKeyDown(KeyCode.Tab)){          
            ToggleMainPannel();
            LoadInInventory();
        }
    }

    public void ToggleMainPannel()
    {
        if
            (MainUiPannel.activeInHierarchy)
        {   
            MainUiPannel.SetActive(false);         
            MainPannelIsOpen = false;
        }
        else
        {
            MainUiPannel.SetActive(true);
            MainPannelIsOpen = true;         
        }
    }

    public void TogglePannel(GameObject PannelToTogle)
    {
        if
           (PannelToTogle.activeInHierarchy)
        {
            PannelToTogle.SetActive(false);
           
        }
        else
        {
            PannelToTogle.SetActive(true);
           
        }
    }

    public void OpenPannel(GameObject PannelToOpen)
    {
        PannelToOpen.SetActive(true);
    }

    public void ClosePannel(GameObject PannelToClose)
    {
        PannelToClose.SetActive(false);
    }

    //TODO this is getting called every frame fix
    public void CheckForOpenUI()
    {
        if (MainPannelIsOpen == true)
        {
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(false);
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.XSensitivity = 0;
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.YSensitivity = 0;
        }else
        {
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(true);
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.XSensitivity = 2;
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.YSensitivity = 2;
        }
    }


    // inventory Options
    public void OpenMainInventoryPannel()
    {
        LoadInInventory();
        TogglePannel(MainInventoryPannel);
        if (MainInventoryPannel.activeInHierarchy) MainInventoryIsOpen = true;
        else MainInventoryIsOpen = false;
       
    }

    public void LoadInInventory()
    {
        foreach (Transform child in ItemParrent.transform)
        {
           
                GameObject.Destroy(child.gameObject);
          
        }
        // if we find  an item that we already have just destroy it 
        for (int i = 0; i < Player.Instance.inventory.Container.Count; i++)
        {         
            GameObject NewButton = Instantiate(ItemButton, ItemParrent.transform);
            NewButton.GetComponent<MainInventoryButton>()._Myobject = Player.Instance.inventory.Container[i].item;
            NewButton.name = NewButton.GetComponent<MainInventoryButton>()._Myobject.name;
            Text AmountText = NewButton.transform.GetChild(0).GetComponent<Text>();
            AmountText.text = Player.Instance.inventory.Container[i].amount.ToString();
            Image IconImage = NewButton.transform.GetChild(1).GetComponent<Image>();
            IconImage.sprite = Player.Instance.inventory.Container[i].Icon;        
        }
        InventoryLoaded = true;
    }

    public void LoadQuestItemInventory()
    {
        foreach (Transform child in ItemParrent.transform)
        {

            GameObject.Destroy(child.gameObject);

        }
        // if we find  an item that we already have just destroy it 
        for (int i = 0; i < Player.Instance.inventory.Container.Count; i++)
        {
            if (Player.Instance.inventory.Container[i].ObjectType == ItemType.QuestItem)
            {
                GameObject NewButton = Instantiate(ItemButton, ItemParrent.transform);
                NewButton.GetComponent<MainInventoryButton>()._Myobject = Player.Instance.inventory.Container[i].item;
                NewButton.name = NewButton.GetComponent<MainInventoryButton>()._Myobject.name;
                Text AmountText = NewButton.transform.GetChild(0).GetComponent<Text>();
                AmountText.text = Player.Instance.inventory.Container[i].amount.ToString();
                Image IconImage = NewButton.transform.GetChild(1).GetComponent<Image>();
                IconImage.sprite = Player.Instance.inventory.Container[i].Icon;


            }
          
        }
        InventoryLoaded = true;
    }

    public void LoadWeaponItemInventory()
    {
        foreach (Transform child in ItemParrent.transform)
        {

            GameObject.Destroy(child.gameObject);

        }
        // if we find  an item that we already have just destroy it 
        for (int i = 0; i < Player.Instance.inventory.Container.Count; i++)
        {
            if (Player.Instance.inventory.Container[i].ObjectType == ItemType.Weapon)
            {
                GameObject NewButton = Instantiate(ItemButton, ItemParrent.transform);
                NewButton.GetComponent<MainInventoryButton>()._Myobject = Player.Instance.inventory.Container[i].item;
                NewButton.name = NewButton.GetComponent<MainInventoryButton>()._Myobject.name;
                Text AmountText = NewButton.transform.GetChild(0).GetComponent<Text>();
                AmountText.text = Player.Instance.inventory.Container[i].amount.ToString();
                Image IconImage = NewButton.transform.GetChild(1).GetComponent<Image>();
                IconImage.sprite = Player.Instance.inventory.Container[i].Icon;


            }

        }
        InventoryLoaded = true;
    }





    public void LoadInExtraInfo(string _DescriptionText,Sprite IconSprite)
    {
        Text DescriptionText = InformationPannel.transform.GetChild(0).GetComponent<Text>();
        DescriptionText.text = _DescriptionText;
        Image IconImage = InformationPannel.transform.GetChild(1).GetComponent<Image>();
        IconImage.sprite = IconSprite;
    }

}

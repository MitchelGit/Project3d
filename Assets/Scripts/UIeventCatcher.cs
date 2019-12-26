using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class UIeventCatcher : MonoBehaviour
    {

        [Header("Crossair and infosettings")]
        GameObject CrossairInfoParrent;
        public Transform CenterPannel;
        public Transform HoverTextParrent;
        public Text infotext;
        public Image ActivationImage;
        public GameObject HoverinfoParrent;

        [Header("inventorysettings")]
        public GameObject ItemButton;
        public GameObject InventoryPannel;
        public GameObject InventoryParrent;
        public GameObject ItemInfoPannel;
        [Header("Questpannel")]
        public GameObject ActivateQuestPannel;
        public GameObject test;
        public List<GameObject> SpawnedQuestPannelsList = new List<GameObject>();

        public List<Objectives> ListOfObjectives = new List<Objectives>();
        [Header("dialog settings")]
        public Text DialogName;
        public Text DialogText;
        public Image DialogImage;


        private static UIeventCatcher _instance;
        public static UIeventCatcher Instance;
        // Start is called before the first frame update


        void Start()
        {
            Instance = this;

         
            InventoryParrent = GameObject.FindGameObjectWithTag("InventoryParrent");
            ItemInfoPannel = GameObject.FindGameObjectWithTag("ItemInfoPannel");
            CrossairInfoParrent = GameObject.FindGameObjectWithTag("CrossairInfoParrent");
            HoverinfoParrent = GameObject.FindGameObjectWithTag("HoverInfoPannel");
            InventoryParrent.SetActive(false);

        }


        void Update()
        {

        }

        public void SetUpHoverinfo(string value,float sectohold)
        {
            
            HoverinfoParrent.GetComponentInChildren<Text>().text = value;
            HoverinfoParrent.GetComponentInChildren<Image>().fillAmount = sectohold;

        }

        public void HideShowHoverInfoPannel(bool Set)
        {
            HoverinfoParrent.SetActive(Set);

        }
        public void UpdateInfoText(string value)
        {
            if (infotext.text != value)
            {
                infotext.text = value;

            }

        }

        public void UpdateActivationImage(float SecToHold)
        {
            ActivationImage.fillAmount = SecToHold;
        }




        public void OpenInventory()
        {
            InventoryParrent.SetActive(true);
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(false);
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.XSensitivity = 0;
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.YSensitivity = 0;

        }
        public void CloseInventoryPannel()
        {

            InventoryParrent.SetActive(false);
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(true);
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.XSensitivity = 2;
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.YSensitivity = 2;
            RessetInventoryItemInfo();
            foreach (Transform child in InventoryPannel.transform)
            {
                if (child.tag != "ItemInfoPannel")
                {
                    GameObject.Destroy(child.gameObject);
                }

            }
        }




        public void SpawnRadialMenu(List<InventorySlot> MyListOfItems)
        {

            OpenInventory();
            for (int i = 0; i < MyListOfItems.Count; i++)
            {
                Debug.Log("Should spawn menu item" + MyListOfItems[i].amount + MyListOfItems[i].name);
                GameObject NewButton = Instantiate(ItemButton, InventoryPannel.transform);
                float theta = (2 * Mathf.PI / MyListOfItems.Count) * i;
                float x = Mathf.Sin(theta);
                float y = Mathf.Cos(theta);
                NewButton.transform.localPosition = new Vector3(x, y, 0f) * 100f;


                Text Nametext = ItemButton.transform.GetChild(0).GetComponent<Text>();
                Text AmountText = ItemButton.transform.GetChild(1).GetComponent<Text>();
                Nametext.text = MyListOfItems[i].name;
                AmountText.text = MyListOfItems[i].amount.ToString();
            }

        }

        // we are spawning a selected inventory based on item type 
        //TODO thinking of populating inventory instead of spawning new one 
        public void SpawnSelectedInventory(List<InventorySlot> MyListOfItems, ItemType Itemtypeinput)
        {

            OpenInventory();
            for (int i = 0; i < MyListOfItems.Count; i++)
            {

                if (MyListOfItems[i].ObjectType == Itemtypeinput)
                {

                    // spawning button for every item in list
                    GameObject NewButton = Instantiate(ItemButton, InventoryPannel.transform);
                    NewButton.GetComponent<ItemButton>()._Myobject = MyListOfItems[i].item;

                    Text AmountText = NewButton.transform.GetChild(0).GetComponent<Text>();
                    AmountText.text = MyListOfItems[i].amount.ToString();
                    //setting the icon
                    NewButton.transform.GetChild(1).GetComponent<Image>().sprite = MyListOfItems[i].item.ItemIcon;

                    //Text Nametext = NewButton.transform.GetChild(0).GetComponent<Text>();
                    //Nametext.text = MyListOfItems[i].item.ItemName;

                    float theta = (2 * Mathf.PI / MyListOfItems.Count) * i;
                    float x = Mathf.Sin(theta);
                    float y = Mathf.Cos(theta);
                    NewButton.transform.localPosition = new Vector3(x, y, 0f) * 150f;

                }

            }

        }

        public void SetupInventoryItemInfo(string titteltext, string descriptiontext)
        {
            ItemInfoPannel.transform.GetChild(0).GetComponent<Text>().text = titteltext;
            ItemInfoPannel.transform.GetChild(1).GetComponent<Text>().text = descriptiontext;

        }
        public void RessetInventoryItemInfo()
        {
            ItemInfoPannel.transform.GetChild(0).GetComponent<Text>().text = "";
            ItemInfoPannel.transform.GetChild(1).GetComponent<Text>().text = "";

        }


        // QUEST SECTION

        public void SpawnQuestInSidelog(Quest QuestToSpawn)
        {
            //TODO finding quest Container => refrence better
            GameObject obj = GameObject.FindGameObjectWithTag("QuestContainer");
            //instantiate a quest pannel
            GameObject SpawnedQpannel = Instantiate(ActivateQuestPannel, obj.transform);
            //setting the name of the pannel to quest name ==> we will use this name later
            SpawnedQpannel.name = QuestToSpawn.name;
            //adding it to a list of quests spawned in the sidepannel
            SpawnedQuestPannelsList.Add(SpawnedQpannel);
            //we find the goal pannel
            GameObject QuestGoalPannel = SpawnedQpannel.transform.GetChild(2).transform.gameObject;
            // we loop true all the questgoals in the quest and span them in the goal pannel
            foreach (QuestObjectiveParrent item in QuestToSpawn.ListOfQuestObjectivesParrents)
            {
                foreach (Objectives _objective in item.ListOfObjectives)
                {
                    GameObject SpawnedObjectivePannel = Instantiate(test, QuestGoalPannel.transform);
                    SpawnedObjectivePannel.name = _objective.name;
                }

            }
        }


        public void UpdateQuestInSideLog(Quest QUestToUpdate)
        {
            // we loop true all the quests in the pannel
            foreach (GameObject var in SpawnedQuestPannelsList)
            {
                // if the name of the quest we are trying to update is correct we start putting in the values
                if (var.name == QUestToUpdate.name)
                {
                    //tittelPannel
                    GameObject TitlePannel = var.transform.GetChild(0).gameObject;
                    //questtitle
                    TitlePannel.transform.GetChild(0).GetComponent<Text>().text = QUestToUpdate.name;
                    //QuestStatusText
                    TitlePannel.transform.GetChild(1).GetComponent<Text>().text = "Active";
                    if (QUestToUpdate.IsComplete == true)
                        TitlePannel.transform.GetChild(1).GetComponent<Text>().text = "Complete";
                    //descriptionPannel
                    GameObject DescriptionPannel = var.transform.GetChild(1).gameObject;
                    DescriptionPannel.transform.GetChild(0).GetComponent<Text>().text = QUestToUpdate.QuestDescription;
                    // questrecieverPannel


                    // goal pannel

                    GameObject QuesGoalPannel = var.transform.GetChild(2).gameObject;
                    // we loop true all the childs in the goalpannel  if our quest has this goal inside it we grab and set the values


                    foreach (QuestObjectiveParrent item in QUestToUpdate.ListOfQuestObjectivesParrents)
                    {
                        foreach (Objectives _objective in item.ListOfObjectives)
                        {
                            if (QuesGoalPannel.transform.Find(_objective.name))
                            {


                                GameObject CurrentGoalPannel = QuesGoalPannel.transform.Find(_objective.name).transform.gameObject;
                                Objectives CurrentObjective = _objective;
                                CurrentGoalPannel.transform.GetChild(1).GetComponent<Text>().text = CurrentObjective.name;
                                if (CurrentObjective.IsComplete == false)
                                {
                                    CurrentGoalPannel.transform.GetChild(0).GetComponent<Image>().fillAmount = 0f;

                                }
                                else
                                {

                                    CurrentGoalPannel.transform.GetChild(1).GetComponent<Text>().text = CurrentObjective.name + "" + "Complete";
                                    CurrentGoalPannel.transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;

                                }




                            }
                        }



                    }






                }
            }
        }

    }




}

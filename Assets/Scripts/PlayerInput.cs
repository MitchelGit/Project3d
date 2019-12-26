using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    [System.Serializable]
    public class PlayerInput : MonoBehaviour
    {
       
        public GameObject CurrentInteractable;     
        public GameObject CurrentHover;
        public LayerMask InteractableLayer;
        public static bool IsUiOpen = false;
       
        public static bool IsInDialog = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RayCastingToCenter();
            CheckForPlayerOpeningInventory();
            CheckForItemDrop();
            CheckForUsingItem();
            Save();
            Load();
            addquest();
            if (IsInDialog == true)
            {
                CheckForNextDialog();
            }else { return; }
           
        }




        public void RayCastingToCenter()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (CurrentHover && CurrentHover.name != hit.transform.gameObject.name) { UIeventCatcher.Instance.UpdateInfoText(""); }
                CurrentHover = hit.transform.gameObject;
                if (CurrentInteractable)
                {
                    CurrentInteractable.GetComponent<Interactable>().HoverOffInteractable();
                    CurrentInteractable = null;
                }

            }
            if (Physics.Raycast(ray, out hit, 5f, InteractableLayer))
            {
                if (hit.transform)
                {
                    //if infinite from camera hits interactable then store it
                    CurrentInteractable = hit.transform.gameObject;
                    Interactable _interactable = CurrentInteractable.transform.GetComponent<Interactable>();
                    _interactable.HoverOverInteractAble(hit);
                    CheckForHoldInteract(_interactable);
                }
            }
        }




        public void CheckForHoldInteract(Interactable __interactable)
        {
            if (Input.GetKey(KeyCode.E) && __interactable.ReadyToInteract == true)
            {

                __interactable.PressAndHoldInteract();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                __interactable.ReleaseInteract();
            }
        }



        public void CheckForItemDrop()
        {

            if (Input.GetKeyDown(KeyCode.G))
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 3f))
                    Debug.DrawRay(ray.origin, ray.direction * 3f, Color.yellow);

                if (hit.transform)
                {
                    Debug.Log(hit.point);
                    Player.Instance.DropActiveItem(hit.point);
                }


            }

        }
        // Inventory section
        public void CheckForPlayerOpeningInventory()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                IsUiOpen = true;
                UIeventCatcher.Instance.SpawnSelectedInventory(transform.GetComponentInParent<Player>().inventory.Container, ItemType.Weapon);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                IsUiOpen = true;

                UIeventCatcher.Instance.SpawnSelectedInventory(transform.GetComponentInParent<Player>().inventory.Container, ItemType.Food);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                IsUiOpen = true;

                UIeventCatcher.Instance.SpawnSelectedInventory(transform.GetComponentInParent<Player>().inventory.Container, ItemType.QuestItem);
            }



            //releasing inventory button
            if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha3))
            {
                // when we hover over an itembutton we select an item if we close the inventory with a selected item we need to spawn it
                if (Player.Instance.CurrentSelectedItem)
                {
                    Player.Instance.SpawnSelectedItem();
                }




                //TODO we can combine these functions here
                UIeventCatcher.Instance.RessetInventoryItemInfo();
                UIeventCatcher.Instance.CloseInventoryPannel();
            }



        }



        public void CheckForUsingItem()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {

                Player.Instance.UseActiveItem();

            }
        }


        // save and load 
        public void Save()
        {

            if (Input.GetKeyDown(KeyCode.O))
            {

                Player.Instance.inventory.Saveinventory();
                Debug.Log("saving");
            }


        }

        public void Load()
        {

            if (Input.GetKeyDown(KeyCode.P))
            {

                Player.Instance.inventory.LoadInventory();
                Debug.Log("loading");
            }



        }




        public void CheckForNextDialog()
        {
           if (Input.GetKeyDown(KeyCode.E)&& IsInDialog == true)
            {
                DialogManager.instance.Dequedialog();
            }
        }



        public void addquest()
        {

            if (Input.GetKeyUp(KeyCode.L))
            {
                Quest current = Player.Instance.questtoadd;
                if (!Player.Instance.Questlog.QuestLogContainer.Contains(current))
                {
                    Player.Instance.Questlog.AddQuest(current);
                }

                //  Player.Instance.Questlog.EvaluateActiveQuest();
            }
        }






    }




}


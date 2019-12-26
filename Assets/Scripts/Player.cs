using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Player : MonoBehaviour
    {
        public InventoryObject inventory;
        public QuestLog Questlog;
        public Quest questtoadd;
        public WeaponObject CurrentWeapon;
        public InventoryObject CurrentInventoryItem;
        public GameObject CurrentActiveItemGameobject;
        public GameObject CurrentQuestReciever;

        public Transform RightHandPosition;

        public ItemObject CurrentSelectedItem;
        public ItemObject CurrentActiveItem;

        public bool HoveringOverUIObject = false;
        public ItemObject CurrentSelectedUIItem;

        Player _instance;
        public static Player Instance;
        // Start is called before the first frame update
        void Start()
        {
            Instance = this;

        }

        // Update is called once per frame
        void Update()
        {

        }


        // When we hover over a inventory item we select it
        public void SelectingItem(ItemObject _SelectedItem)
        {
            CurrentSelectedItem = _SelectedItem;
        }

        public void SwitchObject()
        {
            inventory.AddItem(CurrentActiveItem, 1, CurrentActiveItem.type, CurrentActiveItem.name, CurrentActiveItem.ItemIcon);
            Destroy(CurrentActiveItemGameobject);
            CurrentActiveItemGameobject = null;
            CurrentActiveItem = null;
        }

        public void SpawnSelectedItemAndMakeItActive()
        {
            CurrentActiveItemGameobject = Instantiate(CurrentSelectedItem.Prefab, RightHandPosition);
            CurrentActiveItem = CurrentSelectedItem;
            CurrentActiveItemGameobject.transform.parent = RightHandPosition;
            CurrentSelectedItem = null;
            inventory.RemoveItem(CurrentActiveItem);
        }


        public void SpawnSelectedItem()
        {
            // we check when we close the inventory in the player input with a selected item
            // if we spawn an item but we already have an active item we destroy the active gameobject and add it back to the inventory  
            if (CurrentActiveItem && CurrentActiveItemGameobject)
            {
                SwitchObject();
            }
            // we spawn current selected item and make it active we also remove it from the inventory
            SpawnSelectedItemAndMakeItActive();

        }


        //TODO we need to sort out where to drop the item maybe just drop it instea dos pawning a new one ? 
        public void DropActiveItem(Vector3 PointToDrop)
        {
            Destroy(CurrentActiveItemGameobject);
            GameObject test = Instantiate(CurrentActiveItem.Prefab, new Vector3(PointToDrop.x, PointToDrop.y, PointToDrop.z), CurrentActiveItem.Prefab.transform.rotation);

            CurrentActiveItemGameobject = null;
            CurrentSelectedItem = null;

        }

        public void UseActiveItem()
        {
            // logic for quest item delivery
            if (CurrentActiveItem.type == ItemType.QuestItem)
            {   // we are checking in the quesitem recieverscript if we are hovering over the right position and our active item is the same as the one the reciever is looking for 
                if (CurrentQuestReciever.GetComponent<QuestItemReciever>().ReadyToComplete == true)
                {
                    // then we call the use function on the item
                    CurrentActiveItemGameobject.GetComponent<QuestItem>().Use();
                    CurrentQuestReciever.GetComponent<QuestItemReciever>().TryToComplete();
                }

            }
            else
            {
                Debug.Log("we are using somthing but we dont have annything in hands");
                return;

            }




        }


    }
}

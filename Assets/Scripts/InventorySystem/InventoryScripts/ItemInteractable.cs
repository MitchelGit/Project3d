namespace UnityStandardAssets.Characters.FirstPerson
{
    public class ItemInteractable : Interactable
    {
        public ItemObject _itemObject;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


      

        public override void CompleteInteraction()
        {
            base.CompleteInteraction();

            PickUpItem();
        }


        public void PickUpItem()
        {

            Player.Instance.inventory.AddItem(_itemObject, 1, _itemObject.type, _itemObject.name, _itemObject.ItemIcon);
            Destroy(gameObject);
        }
    }
}

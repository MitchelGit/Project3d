using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class QuestItemReciever : MonoBehaviour
    {
        public bool OnHover = false;
        public bool Iscomplete = false;
        public bool ReadyToComplete = false;
        public ItemObject ItemObjectNeeded;
        public float Distancetointeract = 1f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public virtual void OnHoverOverReciever(RaycastHit Point)
        {

            OnHover = true;
            ItemObject CurrentActiveItem = Player.Instance.CurrentActiveItem;

            if (Point.transform && CurrentActiveItem == ItemObjectNeeded && Point.distance <= Distancetointeract)
            {

                Debug.Log("we are hovering over quest reciever with the right item");

                UIeventCatcher.Instance.UpdateInfoText(" U To Place");
                ReadyToComplete = true;
            }





        }
        //public void OnHoverOfOverReciever(RaycastHit Point)
        //{
        //    HoveringOverCorrectPosition = false;
        //    Debug.Log("hovering over quest reciever");

        //}

        public void TryToComplete()
        {
            if (Iscomplete == false && ReadyToComplete == true)
            {
                Iscomplete = true;
                ReadyToComplete = false;
                CompleteAction();

            }

        }

        public virtual void CompleteAction()
        {




        }



    }

}

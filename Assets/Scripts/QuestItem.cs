using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class QuestItem : MonoBehaviour
    {
        public bool CorrectPosition = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public virtual void Use()
        {
            // we are calling the use function here but it only gets called if the parameters in  player and quesitemreciever are correct
            Debug.Log("using quest item");
            Destroy(Player.Instance.CurrentActiveItemGameobject);
        }

        public void CheckForRightPosition()
        {
            if (CorrectPosition == true)
            {
                Debug.Log("i am inthe right position");
            }
            else
            {
                Debug.Log("i am not in the right p osition");
            }

        }

    }

}

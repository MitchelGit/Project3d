using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.FirstPerson
{

    public class Interactable : MonoBehaviour, IInteractable
    {
        [Header("General Settings")]
        public bool Resset = true;
        public float TimeToResset = 1f;
        public string InfoText = "";
        public float ActivatingDistance = 3f;
        public float SecToHold = 3.0f;
        public bool Activated;

        [Header("Debug settings")]
        public bool HoveringOverInteract = false;
        public bool UseTimerImage = true;
        public bool ReadyToInteract = false;
        public bool Interacting = false;
        
        public GameObject CurrentItem;
     


      
        public float timer = 0;
        private static Interactable _instance;
        public static Interactable Instance;

      
        private void Awake()
        {
            _instance = this;

        }
      
        public virtual void PressAndHoldInteract()
        {
            // if activated or picked up we dont wanne do annything annymore
            if(Activated == true) { return; }
            Interacting = true;
            timer = Mathf.Lerp(0, 1, timer + 1 * Time.deltaTime / SecToHold);

            if (UseTimerImage == true) UIeventCatcher.Instance.UpdateActivationImage(timer);
               
            if (timer == 1 && Interacting == true) CompleteInteraction();
           
             
           


        }
        public virtual void ReleaseInteract()
        {
            timer = 0;
            Interacting = false;
            UIeventCatcher.Instance.UpdateActivationImage(0);
   
        }

        public virtual void CompleteInteraction()
        {     
            if (Activated == true)
            {
                return;
            }
            Activated = true;
            Interacting = false;
            UIeventCatcher.Instance.SetUpHoverinfo("", 0);        

            if (Resset == true)  StartCoroutine(Ressett());
                             
        }

        IEnumerator Ressett()
        {
            yield return new WaitForSeconds(TimeToResset);
            Activated = false;
        }

        public virtual void HoverOverInteractAble(RaycastHit HitPoint)
        {
           if (Activated == true)
            {
                UIeventCatcher.Instance.HideShowHoverInfoPannel(false);
            }else
            {
                UIeventCatcher.Instance.HideShowHoverInfoPannel(true);
            }
         
            if (HitPoint.distance <= ActivatingDistance)
            {
               
                ReadyToInteract = true;
                UIeventCatcher.Instance.SetUpHoverinfo(InfoText, 0);



                if (HitPoint.transform.gameObject.GetComponent<ItemInteractable>())
                {
                    CurrentItem = HitPoint.transform.gameObject;
                }

            }

            // the distance is to great or we have touched somthing else for interaction
            if (HitPoint.distance >= ActivatingDistance || HitPoint.collider.name != transform.name)
            {
                timer = 0;
                ReadyToInteract = false;
                UIeventCatcher.Instance.SetUpHoverinfo("", 0);
               



            }

        }

        public void HoverOffInteractable()
        {
            ReadyToInteract = false;
            UIeventCatcher.Instance.SetUpHoverinfo("", 0);
        }
    }

}

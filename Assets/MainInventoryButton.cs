using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class MainInventoryButton : MonoBehaviour

     , IPointerClickHandler // 2
                            //, IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler


    {
        public ItemObject _Myobject;
        public Image Render;
        public Sprite Defaultimage;
        public Sprite HoverImage;
        public bool IsActive;

        // Start is called before the first frame update
        void Start()
        {
            Render = GetComponent<Image>();
            Render.sprite = Defaultimage;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerClick(PointerEventData eventData) // 3
        {
            print("I was clicked " + _Myobject.name);

            //if (eventData.pointerCurrentRaycast.gameObject.tag == "Field")
            //{
            //    //HERE I WANT TO CALL METHOD, every button I have other method so I need to get it from public field

            //}
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
          
                MainUi.Instance.LoadInExtraInfo(_Myobject.ItemDescription,_Myobject.ItemIcon);
          





            //UIeventCatcher.Instance.SetupInventoryItemInfo(_Myobject.ItemName, _Myobject.ItemDescription);
            //Render.sprite = HoverImage;
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            //Player.Instance.DeSelectingItem();



            //print("hoveraf" + _Myobject.name);
            //UIeventCatcher.Instance.RessetInventoryItemInfo();
            //Render.sprite = Defaultimage;
        }


    }
}

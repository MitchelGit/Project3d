using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventHandler : MonoBehaviour,IPointerDownHandler
{
    public UnityEvent eventhandler;
    public DialogBase MyDialog;

    public void OnPointerDown(PointerEventData eventData)
    {
        eventhandler.Invoke();
        if (MyDialog != null)
        {

            DialogManager.instance.EnqueDialogue(MyDialog);
          
        }
        DialogManager.instance.CloseOptionsPannel();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

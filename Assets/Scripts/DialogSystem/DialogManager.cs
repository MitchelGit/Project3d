using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Events;


public class DialogManager : MonoBehaviour
{
    public float Delay = 0.001f;
    public static DialogManager instance;
    private bool isCurrentlyTyping;
    private string completetext;
    private bool IsDialogOption;
    public bool Uiisopen = false;

    [Header("dialog settings")]
    public Text DialogName;
    public Text DialogText;
    public Image DialogImage;
    public GameObject DialogParrent;
    public GameObject DialogOptionParrent;
    public Queue<DialogBase.DialogInfo>dialoginfo = new Queue<DialogBase.DialogInfo>();
    private bool InDialog;
    public GameObject[] optionButtons;
    private int OptionsAmount;
    public Text QuesionText;

   
    public void EnqueDialogue(DialogBase db)
    {
        Uiisopen = true;
        if (InDialog) return;
        InDialog = true;
        PlayerInput.IsInDialog = true;
        DialogParrent.SetActive(true);
       
        dialoginfo.Clear();

        if (db is DialogOptions)
        {
           
            IsDialogOption = true;
            DialogOptions _dialogoptions = db as DialogOptions;
            OptionsAmount = _dialogoptions.optionsinfo.Length;
            QuesionText.text = _dialogoptions.QuestionText;
           
            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }

            for (int i = 0; i < OptionsAmount; i++)
            {
                
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = _dialogoptions.optionsinfo[i].ButtonName;

                EventHandler myeventhandler = optionButtons[i].GetComponent<EventHandler>();
                myeventhandler.eventhandler = _dialogoptions.optionsinfo[i].MyEvent;
                if (_dialogoptions.optionsinfo[i].NextDialog != null)
                {
                    myeventhandler.MyDialog = _dialogoptions.optionsinfo[i].NextDialog;
                }else
                {
                    myeventhandler.MyDialog = null;
                }
            }
        }
        else
        {
            IsDialogOption = false;
        }

        foreach(DialogBase.DialogInfo info in db.dialoginfo)
        {
            dialoginfo.Enqueue(info);
        }

        Dequedialog();
    }

    public void Dequedialog()
    {
      
        if (isCurrentlyTyping == true)
        {
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;

        }
        if (dialoginfo.Count == 0)
        {
            EndDialogue();
           

            return;
        }

      
        DialogBase.DialogInfo info = dialoginfo.Dequeue();
        completetext = info.Dialogtext;
        DialogName.text = info.MyName;
        DialogText.text = info.Dialogtext;
        DialogImage.sprite = info.Portrait;
        DialogText.text = "";
        StartCoroutine(TypeTextFunction(info));

    }

    private void CompleteText()
    {
        DialogText.text = completetext;
    }
    public void EndDialogue()
    {
        DialogParrent.SetActive(false);
        PlayerInput.IsInDialog = false;
        Uiisopen = false;
        InDialog = false;
        OptionsLogic();
    }


    private void OptionsLogic()
    {
        if (IsDialogOption == true)
        {
            PlayerInput.IsInDialog = true;
            DialogOptionParrent.SetActive(true);
            Uiisopen = true;

        }
      

    }



    IEnumerator TypeTextFunction(DialogBase.DialogInfo infoo)
    {
        isCurrentlyTyping = true;
       
        foreach(char c in infoo.Dialogtext.ToCharArray())
        {
            yield return new WaitForSeconds(Delay);
            DialogText.text += c;
         
        }
        isCurrentlyTyping = false;


    }

    public void CloseOptionsPannel()
    {
        DialogOptionParrent.SetActive(false);
        PlayerInput.IsInDialog = false;
     
     
    }

    // Start is called before the first frame update
    void Start()
    {
        DialogParrent.SetActive (false);
        DialogOptionParrent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Uiisopen == true)
        {
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(false);
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.XSensitivity = 0;
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.YSensitivity = 0;

        }
        else
        {
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(true);
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.XSensitivity = 2;
            GameObject.FindObjectOfType<RigidbodyFirstPersonController>().mouseLook.YSensitivity = 2;
            return;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {

        }
        else
        {
            instance = this;
        }
    }
}

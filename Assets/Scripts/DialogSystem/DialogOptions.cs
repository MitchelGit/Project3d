using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "new dialogue Option", menuName = "dialogues/New DialogOptions")]

public class DialogOptions : DialogBase
{


    [TextArea(2, 10)]
    public string QuestionText;
    [System.Serializable]
  public class Options
    {
        public string ButtonName;
        public DialogBase NextDialog;
        public UnityEvent MyEvent;
        
    }

    public Options[] optionsinfo;

}

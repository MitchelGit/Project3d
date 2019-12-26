using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new dialogue",menuName ="dialogues/New Dialog")]
public class DialogBase : ScriptableObject
{
    [System.Serializable]
    public class DialogInfo
    {
        public string MyName;
        [TextArea(4, 8)]
        public string Dialogtext;
       public Sprite Portrait;
    }
    [Header("insert dialog info here")]
    public DialogInfo[] dialoginfo;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

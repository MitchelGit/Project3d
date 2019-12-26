using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class QuestGiver : Interactable
{
    [Header("QuestSettings")]
    public Quest QuestTogive;
    public bool DeliverdQuest = false;
    public DialogBase MyStartDialogue;
    public DialogBase MyQuestActiveDialog;
    public DialogBase MyCompletedDialogue;
    public DialogBase myselectedDialog;

    public bool DialogTriggerd = false;
    //[Header("General Settings")]
    //public bool Resset = true;
    //public float TimeToResset = 1f;
    //public string InfoText = "";
    //public float ActivatingDistance = 3f;
    //public float SecToHold = 3.0f;

    //[Header("Debug settings")]
    //public bool HoveringOverInteract = false;
    //public bool UseTimerImage = true;
    //public bool ReadyToInteract = false;
    //public bool Interacting = false;
    //public bool Activated;
    //public GameObject CurrentItem;


    //public float timer = 0;
    //private static Interactable _instance;
    //public static Interactable Instance;



    // Start is called before the first frame update
    void Start()
    {
        myselectedDialog = MyStartDialogue;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void CompleteInteraction()
    {
        if (Resset == true) StartCoroutine(Ressett());
        if (QuestTogive.IsActive == true)
        {
            myselectedDialog = MyQuestActiveDialog;
        }
        if (QuestTogive.IsComplete == true)
        {
            myselectedDialog = MyCompletedDialogue;
        }     
        if (Activated == false)
        {
            TriggerDialog();
          
        }
       

    }
  
    public void TriggerDialog()
    {   
        PlayerInput.IsInDialog = true;
        DialogManager.instance.EnqueDialogue(myselectedDialog);
        Activated = true;
    }



    IEnumerator Ressett()
    {
        yield return new WaitForSeconds(TimeToResset);
        Activated = false;
    }
    public void GiveQuest()
    {
        if (DeliverdQuest == false)
        {
          Player.Instance.Questlog.AddQuest(QuestTogive);
          DeliverdQuest = true;

        }else
        {
            EvaluteQuestHandIn();
        }
          
    }

    public void EvaluteQuestHandIn()
    {
     
            if (QuestTogive.IsComplete == true)
            {
                Debug.Log("the quest is complete well done removing quest from active questLog");
                // action here or  follow up quest
            }else
            {
                Debug.Log("the quest is not yet complete my friend come back when you ");
                // actionhere maybe dialog random
            }
        }


}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


[CreateAssetMenu(fileName = "new Event", menuName = "Events/New Event")]

public class EventBehaviour : ScriptableObject
{
   public  Quest QuestToGive;
  public void GiveQuestEvent()
    {
        Player.Instance.Questlog.AddQuest(QuestToGive);
        Debug.Log("should vbe ginving quest here");
    }
}

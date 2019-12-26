using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


[System.Serializable]
public enum QuestObjectiveParrentType
{
    KillObjectiveParrent,
    ActivateObjectiveParrent,
    ExplorationObjectiveParrent,
    CollectObjectiveParrent

}
public abstract class QuestObjectiveParrent : ScriptableObject
{
    // Start is called before the first frame update
    public Quest ConnectedQuest;

    public string QuestObjectiveTitle;
    public QuestObjectiveParrentType ObjectiveType;
    public string QuestObjectiveDescription;
    public bool IsActive = false;
    public bool IsComplete;
    public int TotalObjectives;
    public int ObjectivesComplete;
    public List<Objectives> ListOfObjectives = new List<Objectives>();


    public void OnEnable()
    {

        TotalObjectives = ListOfObjectives.Count;
        IsComplete = false;
        ObjectivesComplete = 0;
    }

    public void EvaluateQuestChildObjectives(Objectives Objectivetoevaluate)
    {
        UIeventCatcher.Instance.UpdateQuestInSideLog(ConnectedQuest);
        foreach (Objectives item in ListOfObjectives)
        {
            if (item == Objectivetoevaluate)
            {
                if (item.IsComplete == true)
                {
                    ObjectivesComplete++;
                }
            }
        }

        if (ObjectivesComplete == TotalObjectives)
        {
            SetCompletion();
        }
    }

    public void SetCompletion()
    {
        Debug.Log("Complete");
        IsComplete = true;
        ConnectedQuest.EvaluateQuest(this);

    }
}

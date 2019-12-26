using UnityEngine;

[System.Serializable]
public enum ObjectiveType
{
    ActivateTrigger,
    DeliverItem,
    Activate


}
 
public abstract class Objectives : ScriptableObject
{
    // Start is called before the first frame update

    public QuestObjectiveParrent ObjectiveParrent;
    public string ObjectiveTitle;
    public bool IsActive;
    public bool IsComplete;


    public ObjectiveType Type;


    private void OnEnable()
    {
        IsComplete = false;
    }

    public virtual void CompleteObjective()
    {
        IsComplete = true;

        ObjectiveParrent.EvaluateQuestChildObjectives(this);
    }

    public virtual void ActivateObjective()
    {

    }
}





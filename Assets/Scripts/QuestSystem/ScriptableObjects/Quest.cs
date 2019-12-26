using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[CreateAssetMenu(fileName = "New Quest", menuName = "QuestSystem/ New Quest")]
[System.Serializable]
public class Quest : ScriptableObject
{

    public bool IsActive;
    public bool IsComplete;
    public string QuestName;
    public string QuestDescription;
    public List<QuestObjectiveParrent> ListOfQuestObjectivesParrents = new List<QuestObjectiveParrent>();
    public int QuestObjectivesToComplete;
    public int QuestObjectivesToCompleted;
    public Quest(List<QuestObjectiveParrent> _ListOfQuestObjectivesparrents, bool _isactive, bool _Iscomplete, string _questname, string _questdescription)
    {

        IsActive = _isactive;
        IsComplete = _Iscomplete;
        QuestName = _questname;
        QuestDescription = _questdescription;
        ListOfQuestObjectivesParrents = _ListOfQuestObjectivesparrents;
    }

    private void OnEnable()
    {
        QuestObjectivesToComplete = ListOfQuestObjectivesParrents.Count;
        IsComplete = false;
        QuestObjectivesToCompleted = 0;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void EvaluateQuest(QuestObjectiveParrent Parrenttoevaluate)
    {
        UIeventCatcher.Instance.UpdateQuestInSideLog(this);
        if (Parrenttoevaluate.IsComplete)
        {

            QuestObjectivesToCompleted++;
        }

        if (QuestObjectivesToComplete == QuestObjectivesToCompleted)
        {
            QuestComplition();
        }


    }

    public void QuestComplition()
    {
        Debug.Log("the entire quest is complete");
        IsComplete = true;

    }


}

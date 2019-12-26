using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[CreateAssetMenu(fileName = "New QuestLog", menuName = "QuestSystem/New Questlog")]
[System.Serializable]
public class QuestLog : ScriptableObject
{
    public List<Quest> QuestLogContainer = new List<Quest>();
    // Start is called before the first frame update



    public void AddQuest(Quest questobject)
    {
        QuestLogContainer.Add(questobject);
       questobject.IsActive = true;
        Debug.Log("adding quest to questlog");
        Debug.Log(questobject.name + questobject.QuestDescription);
        UIeventCatcher.Instance.SpawnQuestInSidelog(questobject);
        UIeventCatcher.Instance.UpdateQuestInSideLog(questobject);
    }

    public void RemoveQuest(Quest questobject)
    {
     
        Debug.Log("checking if quest is complete then remocing it from questlog");
        QuestLogContainer.Remove(questobject);
        // should be removing ui shit here 
    }






    public void OnEnable()
    {

        QuestLogContainer.Clear();


    }

    // Update is called once per frame
    void Update()
    {

    }


}

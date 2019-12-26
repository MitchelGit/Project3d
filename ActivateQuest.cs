using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActivateQuest", menuName = "Inventory System/Items/NewActivateQuest")]

public class ActivateQuest :
{
    public void Awake()
    {
        QuestType = QuestType.ActivateQuest;

    }


}


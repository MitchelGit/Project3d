using UnityEngine;

[CreateAssetMenu(fileName = "New QuestItemObject", menuName = "Inventory System/Items/QuestItem")]

public class QuestItemObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.QuestItem;

    }


}

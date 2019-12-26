using UnityEngine;


public enum ItemType
{
    Food,
    Equipement,
    Default,
    Weapon,
    QuestItem

}
public abstract class ItemObject : ScriptableObject
{
    public GameObject Prefab;
    public ItemType type;
    public string ItemName;
    public Sprite ItemIcon;
    [TextArea(15, 20)]
    public string ItemDescription;
}



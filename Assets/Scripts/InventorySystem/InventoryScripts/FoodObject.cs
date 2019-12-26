using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{


    public int HealValue;
    public void Awake()
    {
        type = ItemType.Food;
    }
}

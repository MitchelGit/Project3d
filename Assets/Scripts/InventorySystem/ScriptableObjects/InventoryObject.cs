using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    private ItemDatabaseObject Database;
    public string SavePath;


    private void OnEnable()
    {

#if UNITY_EDITOR
        Database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/DatabaseItems.asset", typeof(ItemDatabaseObject));
#else
        Database = Resources.Load<ItemDatabaseObject>("DatabaseItems");
#endif
    }

    public void AddItem(ItemObject _item, int _amount, ItemType _objecttype, string _name, Sprite _Icon)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Container.Add(new InventorySlot(Database.GetId[_item], _item, _amount, _objecttype, _name, _Icon));
            Debug.Log("adding" + _item.name + _item.ItemDescription);
        }
    }


    public void Saveinventory()
    {
        string savedata = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, SavePath));
        bf.Serialize(file, savedata);
        file.Close();
    }

    public void LoadInventory()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, SavePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, SavePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }

    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = Database.GetItem[Container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {

    }



    public void RemoveItem(ItemObject _item)
    {

        for (int i = 0; i < Container.Count; i++)
        {

            if (Container[i].item == _item)
            {
                Container[i].RemoveAmount(1);
                if (Container[i].amount <= 0)
                {
                    Container.Remove(Container[i]);
                }

                break;
            }
        }






    }


}




[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public ItemType ObjectType;
    public string name;
    public Sprite Icon;
    public int ID;
    public InventorySlot(int _id, ItemObject _item, int _amount, ItemType _objecttype, string _name, Sprite _Icon)
    {
        item = _item;
        amount = _amount;
        ObjectType = _objecttype;
        name = _name;
        Icon = _Icon;
        ID = _id;
    }

    public void AddAmount(int value)
    {
        amount += value;

    }
    public void RemoveAmount(int value)
    {
        amount -= value;

    }


}
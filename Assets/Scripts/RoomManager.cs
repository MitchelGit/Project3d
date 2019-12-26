//Script name : CustomList.cs
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> ListOfInteractObjects = new List<GameObject>();


    public void Start()
    {
        GrabAllInteractables();
    }



    public void GrabAllInteractables()
    {
        MonoBehaviour[] sceneObjects = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < sceneObjects.Length; i++)
        {
            MonoBehaviour currentObj = sceneObjects[i];
            IInteractable currentComponent = currentObj.GetComponent<IInteractable>();

            if (currentComponent != null)
            {
                ListOfInteractObjects.Add(currentObj.transform.gameObject);
                // Debug.Log("added somthing to list" + currentObj.name);

            }
        }
    }


}


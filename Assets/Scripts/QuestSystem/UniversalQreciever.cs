using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UniversalQreciever : Interactable
{
    public Objectives ConnectedObjective;
    public bool Deliverd = false;
    //  public QuestGoal Connectedgoal;
    public bool PlayerTriggerd = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            PlayerTriggerd = true;
            if (ConnectedObjective.Type == ObjectiveType.ActivateTrigger && PlayerTriggerd == true)
            {

                ConnectedObjective.CompleteObjective();
            }
        }

    }

    public override void CompleteInteraction()
    {
        base.CompleteInteraction();
        Debug.Log("complete");
        if (Deliverd == false)
        {
            ConnectedObjective.CompleteObjective();
            Deliverd = true;
        }




    }

    public override void ReleaseInteract()
    {
        base.ReleaseInteract();
    }


}

using UnityEngine;
using System.Collections;

public class BMLNetBehaviour : MonoBehaviour {

    public bool active = false;
    public string characterId = "";
    public string behaviourId = "";
    public string eventName = "";

    public delegate void BehaviourCompleted(BMLNetBehaviour obj, string characterId, string behaviourId, string eventName);
    public BehaviourCompleted OnBehaviourCompleted;  // controller.OnBehaviourCompleted += TriggerEvent in VirtualHumanController.cs

    public void SetBMLNetParam(string characterId, string behaviourId, string eventname)
    {
        Debug.Log("set parameter " + characterId + " " + behaviourId + " " + eventname);
        active = true;
        this.characterId = characterId;
        this.behaviourId = behaviourId;
        this.eventName = eventname;
    }

    protected void TriggerEvent()
    {
        if (OnBehaviourCompleted != null)
        {
            Debug.Log("Trigger Event " + this.GetType() + " " + characterId + " " + behaviourId + " " + eventName);
            OnBehaviourCompleted(this, characterId, behaviourId, eventName);
        }
    }

}

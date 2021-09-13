using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectDialogConditioner : MonoBehaviour
{
    private TriggerDialog trigger;
    private BoxCollider box;
    //public string id;
    public DialogAndConditions [] possibleDialogs;
    
    void Start()
    {
        trigger = GetComponent<TriggerDialog>();
        box = GetComponent<BoxCollider>();
        box.enabled = false;
    }

    
    public void ValidateCondition(string intro)
    {
        for(int i=0; i<possibleDialogs.Length; i++)
            possibleDialogs[i].TryValidation(intro);
    }

    /*public void TryDialogs()
    {
        for(int i=0; i<possibleDialogs.Length; i++)
        {

        }
    }*/
}

[System.Serializable]
public class DialogAndConditions
{
    public Dialog dialog;
    public Condition [] conditions;

    public void TryValidation(string intro)
    {
        for(int i=0; i<conditions.Length; i++)
            conditions[i].TryValidation(intro);
    }

    public bool Check()
    {
        for(int i=0; i<conditions.Length; i++)
            if(!conditions[i].Check())  return false;

        return true;
    }
}

[System.Serializable]
public class Condition
{
    public string id;
    public bool state = false;

    public void TryValidation(string intro)
    {
        if(intro == id) state = true;
    }

    public bool Check()
    {
        return state;
    }
}

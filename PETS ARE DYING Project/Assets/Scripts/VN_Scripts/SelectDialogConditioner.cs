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

        //If there are no dialogs, the SDC object must not work and stop the normal running of the TriggerDialog
        if(possibleDialogs!=null && possibleDialogs.Length!=0)
            box.enabled = false;
    }

    
    public void ValidateCondition(string intro)
    {
        Debug.Log("ValidateCondition IN SelectDialogConditioner ACTIVATED");
        for(int i=0; i<possibleDialogs.Length; i++)
            possibleDialogs[i].TryValidation(intro);

        TryDialogs();

        Debug.Log("ValidateCondition IN SelectDialogConditioner FINISHED");
    }

    public void TryDialogs()
    {
        foreach(DialogAndConditions dac in possibleDialogs)
        {
            if(dac.Check())
            {
                //Enable trigger
                box.enabled = true;
                //Sustitute dialog
                trigger.dialog = dac.dialog;
            }
        }
    }
}

[System.Serializable]
public class DialogAndConditions
{
    public Dialog dialog;
    public Condition [] conditions;

    public void TryValidation(string intro)
    {
        Debug.Log("TryValidation IN DialogAndConditions ACTIVATED");
        for(int i=0; i<conditions.Length; i++)
            conditions[i].TryValidation(intro);

        Debug.Log("TryValidation IN DialogAndConditions FINISHED");
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
        Debug.Log("TryValidation IN Condition ACTIVATED");
        if(intro == id) state = true;
        Debug.Log("TryValidation IN Condition FINISHED");
    }

    public bool Check()
    {
        return state;
    }
}

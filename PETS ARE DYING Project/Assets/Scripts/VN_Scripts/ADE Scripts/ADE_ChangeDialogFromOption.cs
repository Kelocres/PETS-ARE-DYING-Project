using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADE_ChangeDialogFromOption : AfterDialogEvent
{
    public Dialog newDialog;
    public TriggerDialog trigger;
    public string nameOption;

    public override void Activate()
    {
        Debug.Log("ADE_ChangeDialogFromOption activated");
        //option.newDialog = newDialog;
        DecisionLine decision = trigger.dialog.decision;
        for(int i=0; i<decision.options.Length; i++)
        {
            if(decision.options[i].showOption == nameOption)
                decision.options[i].newDialog = newDialog;
        }
    }
}
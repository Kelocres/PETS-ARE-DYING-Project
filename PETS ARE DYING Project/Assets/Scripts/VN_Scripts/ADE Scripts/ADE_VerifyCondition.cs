using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADE_VerifyCondition : AfterDialogEvent
{
    public string [] conditions;
    public Dialog dialog;

    public override void Activate()
    {
        Debug.Log("Ade_VerifyCondition ACTIVATED");
        PlayerData pdata = FindObjectOfType<PlayerData>();
        foreach(string cond in conditions)
            pdata.UnlockCondition(cond);

        //Start Dialog (if there is any)
        FindObjectOfType<DialogManager>().StartDialog(dialog);
        Debug.Log("Ade_VerifyCondition FINISHED");
    }
}
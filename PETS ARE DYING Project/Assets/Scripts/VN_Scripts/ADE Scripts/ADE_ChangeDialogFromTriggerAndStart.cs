using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADE_ChangeDialogFromTriggerAndStart :  AfterDialogEvent
{
    public Dialog newDialog;
    public TriggerDialog trigger;

    public override void Activate()
    {
        trigger.dialog = newDialog;
        FindObjectOfType<DialogManager>().StartDialog(newDialog);
    }
}
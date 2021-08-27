using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADE_PointsDuringDialog : AfterDialogEvent
{
    public int points = 0;
    public Dialog nextDialog;
    public bool doNextDialog = true;

    public override void Activate()
    {
        FindObjectOfType<PlayerData>().PointsDuringDialog(points);
        if(doNextDialog)
            FindObjectOfType<DialogManager>().StartDialog(nextDialog);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpTest : SetUpScene
{
    public Transform enterPosition;
    private GameObject player;

    DialogManager dManager;
    public Dialog startingDialog;
    public bool dM_doStartingDialog;

    public override void StartMoment()
    {
        //Sets the initial values of the variables for the scene in a particular moment/day
        dManager = FindObjectOfType<DialogManager>();
        if(dM_doStartingDialog)     
        {
            dManager.startingDialog = startingDialog;
            dManager.doStartingDialog = dM_doStartingDialog;
        }
    }
}

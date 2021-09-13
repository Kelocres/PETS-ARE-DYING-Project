using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpFactory : SetUpScene
{
    public Transform enterPosition;
    private GameObject player;

    DialogManager dManager;
    public Dialog startingDialog;
    public bool dM_doStartingDialog;

    public TriggerDialog dtCoworkers;
    public Dialog dtCoworkers_dialog;
    public string dtCoworkers_information;
    public bool dtCoworkers_onlyOne;

    public TriggerDialog dtSupervisor;
    public Dialog dtSupervisor_dialog;
    public string dtSupervisor_information;
    public bool dtSupervisor_onlyOne;

    public TriggerDialog dtMachines;
    public Dialog dtMachines_dialog;
    public string dtMachines_information;
    public bool dtMachines_onlyOne;

    public override void StartMoment()
    {
        dManager = FindObjectOfType<DialogManager>();
        if(dM_doStartingDialog)     
        {
            dManager.startingDialog = startingDialog;
            dManager.doStartingDialog = dM_doStartingDialog;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = enterPosition.position;

        SetUpTriggerDialog(dtCoworkers, dtCoworkers_dialog, 
        dtCoworkers_information, dtCoworkers_onlyOne);
        SetUpTriggerDialog(dtSupervisor, dtSupervisor_dialog,
        dtSupervisor_information, dtSupervisor_onlyOne);
        SetUpTriggerDialog(dtMachines, dtMachines_dialog,
        dtMachines_information, dtMachines_onlyOne);

    }

    public override void SaveMoment()
    {

    }

    public override void LoadMoment()
    {

    }
}

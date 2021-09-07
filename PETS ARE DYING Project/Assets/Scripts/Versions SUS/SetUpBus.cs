using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpBus : SetUpScene
{
    public Transform enterPosition;
    private GameObject player;

    DialogManager dManager;
    public Dialog startingDialog;
    public bool dM_doStartingDialog;

    public TriggerDialog dtWomanChild;
    public Dialog dtWomanChild_dialog;
    public string dtWomanChild_information;
    public bool dtWomanChild_onlyOne;

    public TriggerDialog dtTeens;
    public Dialog dtTeens_dialog;
    public string dtTeens_information;
    public bool dtTeens_onlyOne;

    public TriggerDialog dtTeacher;
    public Dialog dtTeacher_dialog;
    public string dtTeacher_information;
    public bool dtTeacher_onlyOne;

    public TriggerDialog dtOtherPeople;
    public Dialog dtOtherPeople_dialog;
    public string dtOtherPeople_information;
    public bool dtOtherPeople_onlyOne;

    public TriggerDialog exit;
    public Dialog exit_dialog;
    public string exit_information;
    public bool exit_onlyOne;

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

        SetUpTriggerDialog(dtWomanChild, dtWomanChild_dialog, 
        dtWomanChild_information, dtWomanChild_onlyOne);
        SetUpTriggerDialog(dtTeens, dtTeens_dialog, 
        dtTeens_information, dtTeens_onlyOne);
        SetUpTriggerDialog(dtTeacher, dtTeacher_dialog, 
        dtTeacher_information, dtTeacher_onlyOne);
        SetUpTriggerDialog(dtOtherPeople, dtOtherPeople_dialog,
        dtOtherPeople_information, dtOtherPeople_onlyOne);
        SetUpTriggerDialog(exit, exit_dialog, exit_information, exit_onlyOne);

    }

    public override void SaveMoment()
    {

    }

    public override void LoadMoment()
    {

    }
}

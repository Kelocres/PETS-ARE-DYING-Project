using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpMCHouse : SetUpScene
{
    //Variables for the day
    public Dialog dialogManager_startinDialog;
    public bool dialogManager_doStartingDialog;
    public Dialog squareTrigger_Armchair_dialog;
    public bool squareTrigger_Armchair_onlyOnce;

    private DialogManager dManager;
    private TriggerDialog td_Armchair;

    /*public void Start()
    {
        Debug.Log("Start of SetUpMCHouse");
        dManager = FindObjectOfType<DialogManager>();
        td_Armchair = GameObject.FindGameObjectWithTag("SquareTrigger_Armchair").GetComponent<TriggerDialog>();

    }*/
    
    public override void StartMoment()
    {
        dManager = FindObjectOfType<DialogManager>();
        td_Armchair = GameObject.FindGameObjectWithTag("SquareTrigger_Armchair").GetComponent<TriggerDialog>();
        
        dManager.startingDialog = dialogManager_startinDialog;
        dManager.doStartingDialog = dialogManager_doStartingDialog;

        td_Armchair.dialog = squareTrigger_Armchair_dialog;
        td_Armchair.onlyOnce = squareTrigger_Armchair_onlyOnce;

        //Set value in order to declarate that

    }

    public override void SaveMoment()
    {
        bool currentDoStartingDialog = dManager.doStartingDialog;
        if(currentDoStartingDialog)
            PlayerPrefs.SetString("doStartingDialog","true");
        else
            PlayerPrefs.SetString("doStartingDialog","false");

        bool currentArmchair_enabled = td_Armchair.GetComponent<BoxCollider>().enabled;
        if(currentArmchair_enabled)
            PlayerPrefs.SetString("armchair_enabled", "true");
        else
            PlayerPrefs.SetString("armchair_enabled", "false");
    }

    public override void LoadMoment()
    {
        string savedDoStartingDialog = PlayerPrefs.GetString("doStartingDialog","");
        if(savedDoStartingDialog == "true")
            dManager.doStartingDialog = true;
        else
            dManager.doStartingDialog = false;

        string savedArmchair_enabled = PlayerPrefs.GetString("armchair_enabled","");
        if(savedArmchair_enabled == "true")
            td_Armchair.GetComponent<BoxCollider>().enabled = true;
        else
            td_Armchair.GetComponent<BoxCollider>().enabled = false;


        
    }

}

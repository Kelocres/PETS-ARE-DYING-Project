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
    public string exitToScene_nextScene;
    public bool exitToScene_allowExit;

    public DialogManager dManager;
    public TriggerDialog td_Armchair;
    public ExitToScene exit;

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

        exit.nextScene = exitToScene_nextScene;
        exit.allowExit = exitToScene_allowExit;

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

        string currentNextScene = exit.nextScene;
        PlayerPrefs.SetString("MCHouse_nextScene", currentNextScene);

        bool currentAllowExit = exit.allowExit;
        if(currentAllowExit)
            PlayerPrefs.SetString("MCHouse_allowExit","true");
        else
            PlayerPrefs.SetString("MCHouse_allowExit","false");

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

        exit.nextScene = PlayerPrefs.GetString("MCHouse_nextScene","");

        string savedAllowExit = PlayerPrefs.GetString("MCHouse_allowExit","");
        if(savedAllowExit == "true")
            exit.allowExit = true;
        else
            exit.allowExit = false;
        
    }

}

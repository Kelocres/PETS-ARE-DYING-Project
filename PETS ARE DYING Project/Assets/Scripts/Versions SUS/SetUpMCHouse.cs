using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpMCHouse : SetUpScene
{
    //Variables for the day
    public Dialog dialogManager_startinDialog;
    //public string dialogManager_information;
    public bool dialogManager_doStartingDialog;

    public Dialog squareTrigger_Armchair_dialog;
    public string squareTrigger_Armchair_information;
    public bool squareTrigger_Armchair_onlyOnce;

    public Dialog exitToScene_dialog;
    public string exitToScene_information;
    public bool exitToScene_onlyOnce;

    public Dialog squareTrigger_dog_dialog;
    public string squareTrigger_dog_information;
    public bool squareTrigger_dog_onlyOnce;

    public Dialog squareTrigger_kitchen_dialog;
    public string squareTrigger_kitchen_information;
    public bool squareTrigger_kitchen_onlyOnce;

    public DialogManager dManager;
    public TriggerDialog td_Armchair;
    public TriggerDialog td_dog;
    public TriggerDialog exit;
    public TriggerDialog td_kitchen;


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
        td_Armchair.information = squareTrigger_Armchair_information;
        td_Armchair.onlyOnce = squareTrigger_Armchair_onlyOnce;

        exit.dialog = exitToScene_dialog;
        exit.information = exitToScene_information;
        exit.onlyOnce = exitToScene_onlyOnce;

        td_dog.dialog = squareTrigger_dog_dialog;
        td_dog.information = squareTrigger_dog_information;
        td_dog.onlyOnce = squareTrigger_dog_onlyOnce;

        td_kitchen.dialog = squareTrigger_kitchen_dialog;
        td_kitchen.information = squareTrigger_kitchen_information;
        td_kitchen.onlyOnce = squareTrigger_kitchen_onlyOnce;

        //Set value in order to declarate that

    }

    public override void SaveMoment()
    {
        /*bool currentDoStartingDialog = dManager.doStartingDialog;
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
            PlayerPrefs.SetString("MCHouse_allowExit","false");*/

    }

    public override void LoadMoment()
    {
        /*
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
            exit.allowExit = false;*/
        
    }

}

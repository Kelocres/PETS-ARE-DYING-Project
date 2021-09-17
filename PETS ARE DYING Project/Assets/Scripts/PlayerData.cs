using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int points = 0;

    //Conditions for locked dialogs and options
    public List<string> unlockedConditions;

    //All SelectDialogConditioner in the current scene
    SelectDialogConditioner [] allSDC;
    List<ConditionedDecision> allCD;

    public void PointsDuringDialog(int intro)
    {
        points += intro;
        Debug.Log("Current points = "+points);
    }

    public void UnlockCondition(string cond)
    {
        Debug.Log("UnlockCondition IN PlayerData ACTIVATED");
        if(unlockedConditions==null)
            unlockedConditions = new List<string>();

        unlockedConditions.Add(cond);

        //SelectDialogConditioner [] allSDC = FindObjectsOfType<SelectDialogConditioner>();
        if(allSDC==null)    //THIS IS JUST FOR TESTING IN ONE SCENE
        {
            allSDC = FindObjectsOfType<SelectDialogConditioner>();
            //allCD = FindObjectsOfType<ConditionedDecision>();
            GetAllConditionedDecisions();
        }

        for(int i=0; i<allSDC.Length; i++)
            allSDC[i].ValidateCondition(cond);

        for(int i=0; i<allCD.Count; i++)
            allCD[i].ValidateCondition(cond);

        Debug.Log("UnlockCondition IN PlayerData FINISHED");
    }

    //Must be executed each time it enters in a scene
    public void CheckAllConditions()
    {
        allSDC = FindObjectsOfType<SelectDialogConditioner>();

        for(int i=0; i<allSDC.Length; i++)
            foreach(string condition in unlockedConditions)
                allSDC[i].ValidateCondition(condition);

        //allCD = FindObjectsOfType<ConditionedDecision>();
        GetAllConditionedDecisions();

        for(int i=0; i<allCD.Count; i++)
            foreach(string condition in unlockedConditions)
                allCD[i].ValidateCondition(condition);
    }

    public void GetAllConditionedDecisions()
    {
        //Get all Dialogs in the scene
        //If their afterDialog is a ConditionedDecision, put it in the list
        Dialog [] allDialogs = FindObjectsOfType<Dialog>();
        allCD = new List<ConditionedDecision>();
        foreach(Dialog dia in allDialogs)
            if(dia.afterDialogIsConditionedDecision())
                allCD.Add(dia.conditionedDecision);
    }
}

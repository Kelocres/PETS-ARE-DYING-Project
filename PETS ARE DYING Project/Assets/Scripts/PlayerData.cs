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

    public void PointsDuringDialog(int intro)
    {
        points += intro;
        Debug.Log("Current points = "+points);
    }

    public void UnlockCondition(string cond)
    {
        if(unlockedConditions==null)
            unlockedConditions = new List<string>();

        unlockedConditions.Add(cond);

        //SelectDialogConditioner [] allSDC = FindObjectsOfType<SelectDialogConditioner>();
        if(allSDC==null)    //THIS IS JUST FOR TESTING IN ONE SCENE
            allSDC = FindObjectsOfType<SelectDialogConditioner>();

        for(int i=0; i<allSDC.Length; i++)
            allSDC[i].ValidateCondition(cond);
    }

    //Must be executed each time it enters in a scene
    public void CheckAllConditions()
    {
        allSDC = FindObjectsOfType<SelectDialogConditioner>();

        for(int i=0; i<allSDC.Length; i++)
            foreach(string condition in unlockedConditions)
                allSDC[i].ValidateCondition(condition);
    }
}

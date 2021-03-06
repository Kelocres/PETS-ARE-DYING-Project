using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    
    public DialogLine[] dialogLines;

    public DecisionLine decision;

    public ConditionedDecision conditionedDecision;

    public Dialog nextDialog;

    public AfterDialogEvent ade;

    public int selectBGImage = -1;

    //In case it doesn't get that decision is null (AN ENUM IS BETTER)
    //public bool finish_dialog = false;
    //REFERENCE FOR ENUMS IN UNITY C#:
    //https://learn.unity.com/tutorial/enumerations#5c8a6ee6edbc2a067d47537b
    public enum ActionDialog : short {Finish = 0, Decision = 1, NextDialog = 2, Event = 3, ConditionedDecision = 4};
    public ActionDialog afterDialog = ActionDialog.Finish;

    //Because other scripts cannot work with foreign enums, the comparison
    //will be done from this script

    public bool afterDialogIsFinish()               {return afterDialog == ActionDialog.Finish;}
    public bool afterDialogIsDecison()              {return afterDialog == ActionDialog.Decision;}
    public bool afterDialogIsNextDialog()           {return afterDialog == ActionDialog.NextDialog;}
    public bool afterDialogIsEvent()                {return afterDialog == ActionDialog.Event;}
    public bool afterDialogIsConditionedDecision()  {return afterDialog == ActionDialog.ConditionedDecision;}
    
}

[System.Serializable]
public class DialogLine
{
    //Name of the character who is speaking
    public string name;

    [TextArea(3, 10)] public string text;
}

[System.Serializable]
public class DecisionLine : DialogLine
{
    //public OptionDialog optionA;
    //public OptionDialog optionB;
    public OptionDialog [] options;

}

[System.Serializable]
public class OptionDialog
{
    [TextArea(1, 10)] public string showOption;
    public Dialog newDialog;
    public int points = 0;  //if points!=0, add or subtract those points
    //[TextArea(3, 10)] public string shortExplanation; //if shortExplanation!="", read it like a DialogLine with name=""
}

//Decisions and options with conditions
[System.Serializable]
public class ConditionedDecision : DecisionLine
{
    public ConditionedOption [] conditionedOptions;

    public void ValidateCondition(string intro)
    {
        for(int i=0; i<conditionedOptions.Length; i++)
            conditionedOptions[i].TryValidation(intro);

        TryOptions();

    }

    public void TryOptions()
    {
        //Empty the options array
        options = new OptionDialog [conditionedOptions.Length];
        int cont = 0;

        foreach(ConditionedOption co in conditionedOptions)
        {
            if(co.Check())
            {
                options[cont++] = co;
            }
        }
    }

}

[System.Serializable]
public class ConditionedOption : OptionDialog
{
    public Condition [] conditions;

    //Copy and paste from DialogAndConditions
    public void TryValidation(string intro)
    {
        Debug.Log("TryValidation IN DialogAndConditions ACTIVATED");
        for(int i=0; i<conditions.Length; i++)
            conditions[i].TryValidation(intro);

        Debug.Log("TryValidation IN DialogAndConditions FINISHED");
    }

    public bool Check()
    {
        if(conditions!=null && conditions.Length>0)
            for(int i=0; i<conditions.Length; i++)
                if(!conditions[i].Check())  return false;

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    
    public DialogLine[] dialogLines;

    public DecisionLine decision = null;

    public int selectBGImage = -1;

    //In case it doesn't get that decision is null
    public bool finish_dialog = false;
    
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
    public OptionDialog optionA;
    public OptionDialog optionB;

}

[System.Serializable]
public class OptionDialog
{
    [TextArea(1, 10)] public string showOption;
    public Dialog newDialog;
}

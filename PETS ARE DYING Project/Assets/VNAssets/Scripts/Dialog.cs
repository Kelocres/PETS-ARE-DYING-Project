using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    
    public DialogLine[] dialogLines;

    public DecisionScript decision;

    public int selectBGImage = -1;
    
}

[System.Serializable]
public class DialogLine
{
    //Name of the character who is speaking
    public string name;

    [TextArea(3, 10)] public string text;
}

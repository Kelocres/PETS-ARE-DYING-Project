using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADE_ChangeNextScene : AfterDialogEvent
{

    public string nextScene;
    public ExitToScene exit;

    // Start is called before the first frame update
    public override void Activate()
    {   
        Debug.Log("exit.nextScene's current value: " + exit.nextScene);
        exit.nextScene = nextScene;
        Debug.Log("exit.nextScene's new value: " + exit.nextScene);

    }
}

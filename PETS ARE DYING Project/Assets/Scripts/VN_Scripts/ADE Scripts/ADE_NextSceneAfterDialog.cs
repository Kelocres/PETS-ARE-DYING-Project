using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ADE_NextSceneAfterDialog : AfterDialogEvent
{
    public string nextScene;

    public override void Activate()
    {
        InteractScript interact = FindObjectOfType<InteractScript>();
        interact.nameNextScene = nextScene;
        interact.LoadNextScene();
    }
}

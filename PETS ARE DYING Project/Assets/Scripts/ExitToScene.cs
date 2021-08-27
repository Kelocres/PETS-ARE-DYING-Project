using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToScene : MonoBehaviour
{
    public string nextScene;
    public bool allowExit = true;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && allowExit)
        {
            other.GetComponent<ChangeScene>().SetUpButton(nextScene);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<ChangeScene>().UnableButton();
        }
    }
}

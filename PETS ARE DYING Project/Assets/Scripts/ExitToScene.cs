using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToScene : MonoBehaviour
{
    public string nextScene;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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

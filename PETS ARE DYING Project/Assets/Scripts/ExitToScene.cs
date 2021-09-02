using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToScene : MonoBehaviour
{
    public string nextScene;
    public string information;
    public bool allowExit = true;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && allowExit)
        {
            other.GetComponent<InteractScript>().SetUpButtonForNextScene(nextScene, information);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<InteractScript>().UnableButton();
        }
    }
}

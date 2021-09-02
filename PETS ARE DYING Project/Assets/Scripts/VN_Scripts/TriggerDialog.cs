using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public string information;
    public Dialog dialog;
    public bool onlyOnce = true;

    //void OnTriggerEnter2D(Collider2D other)
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision triggered");
        if(other.tag == "Player")
        {
            /*
            FindObjectOfType<DialogManager>().StartDialog(dialog);
            if(onlyOnce)
                //GetComponent<BoxCollider2D>().SetActive(false);   //THIS ONLY WORKS FOR GAMEOBJECTS
                //GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            */
            FindObjectOfType<InteractScript>().SetUpButtonForDialog(this);

        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision triggered");
        if(other.tag == "Player")
        {
            
            FindObjectOfType<InteractScript>().UnableButton();

        }
    }

    public void Loaded()
    {
        if(onlyOnce)    GetComponent<BoxCollider>().enabled = false;
    }
}

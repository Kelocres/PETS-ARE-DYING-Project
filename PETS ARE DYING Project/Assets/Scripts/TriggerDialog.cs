using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public Dialog dialog;
    public bool onlyOnce = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision triggered");
        if(other.tag == "Player")
        {
            
            FindObjectOfType<DialogManager>().StartDialog(dialog);
            if(onlyOnce)
                //GetComponent<BoxCollider2D>().SetActive(false);   //THIS ONLY WORKS FOR GAMEOBJECTS
                GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

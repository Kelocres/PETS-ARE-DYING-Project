using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNotFollows : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //The camera stops following the player
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //The camera follows the player again
        }
    }
}

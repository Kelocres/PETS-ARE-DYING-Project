using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADE_Example : AfterDialogEvent
{
    public GameObject element;

    public override void Activate()
    {
        Debug.Log("Element disappears!!!!!!");
        element.SetActive(false);
    }
}

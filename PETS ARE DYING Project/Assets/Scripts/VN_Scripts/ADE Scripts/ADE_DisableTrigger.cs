using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADE_DisableTrigger : AfterDialogEvent
{
    public TriggerDialog trigger;
    // Start is called before the first frame update
    public override void Activate()
    {
        trigger.GetComponent<BoxCollider>().enabled = false;
    }
}

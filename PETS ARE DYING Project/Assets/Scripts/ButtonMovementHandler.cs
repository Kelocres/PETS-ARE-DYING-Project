using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMovementHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    public string dir;
    private PlayerMovement2 pm2;

    void Start()
    {
        pm2 = FindObjectOfType<PlayerMovement2>();
    }

    public void OnPointerDown(PointerEventData eventData){
     //buttonPressed = true;
        pm2.InputButtonsMovement(dir, true);
    }
    
    public void OnPointerUp(PointerEventData eventData){
        //buttonPressed = false;
        pm2.InputButtonsMovement(dir, false);

    }
}

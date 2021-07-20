using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNManagerScript : MonoBehaviour
{
    //Arrays of game elements
    //public DecisionScript [] listDecision;
    //public OptionScript [] listOption;
    public Image[] listBackgroundImages;

    //Canvas element in which put the background images
    public Image canvas_bgImage;

    //Just for trying the code during this firsts days
    public 

    // Start is called before the first frame update
    void Start()
    {
        //FindGameObjectWithTag doesn't work with UI.Image
        //canvas_bgImage = GameObject.FindGameObjectWithTag("BackgroundImage");
        GameObject getObject = GameObject.FindGameObjectWithTag("BackgroundImage");
        if(getObject!=null) canvas_bgImage = getObject.GetComponent<Image>();
    }

    public void StartDialog()
    {
        
    }


}

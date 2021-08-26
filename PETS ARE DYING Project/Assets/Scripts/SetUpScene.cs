using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpScene : MonoBehaviour
{
    //General variables
    string moment;

    public virtual void StartMoment()
    {
        //Sets the initial values of the variables for the scene in a particular moment/day
    }

    public virtual void SaveMoment()
    {
        //It gets the current values of the variables and stores them in PlayerPrefs
    }

    public virtual void LoadMoment()
    {
        //Gets the values from PlayerPrefs and updates the relevant variables
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int points = 0;

    public void PointsDuringDialog(int intro)
    {
        points += intro;
        Debug.Log("Current points = "+points);
    }
}

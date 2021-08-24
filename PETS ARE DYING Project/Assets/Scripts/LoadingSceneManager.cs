using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    PlayerData playerData;

    //void onEnable()
    void Start()
    {
        Debug.Log("LOADING SCENE MANAGER");
        //1º:   Getting the relevant GameObjects
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();

        //2º:   Check if the game has started
        //      (if not, create the keys in PlayerPrefs, with the initial values)
        if(!PlayerPrefs.HasKey("startedGame") || PlayerPrefs.GetString("startedGame","")!="true")
        {
            PlayerPrefs.SetString("startedGame","true");
            PlayerPrefs.SetInt("points",0);
        }

        //3º:   Read the values in PlayerPrefs
        playerData.points = PlayerPrefs.GetInt("points",0);
        Debug.Log("Player Points: "+ playerData.points);
    }

    public void SaveBeforeNextScene()
    {
        //Store the values in the keys of PlayerPrefs
        PlayerPrefs.SetInt("points",playerData.points);
    }
}

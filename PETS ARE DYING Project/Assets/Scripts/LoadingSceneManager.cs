using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    PlayerData playerData;
    public SetUpScene setUp;
    public bool resetPlayerPrefs;

    //void onEnable()
    void Start()
    {
        /*if(resetPlayerPrefs)
        {
            //PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteKey("startedGame");
        }*/

        Debug.Log("Start of LoadingSceneManager");
        //Debug.Log("LOADING SCENE MANAGER");
        //setUp = GameObject.FindGameObjectWithTag("SetUpScene_Day1").GetComponent<SetUpScene>();
        //About the SetUpScene:
        setUp.StartMoment();
        //1º:   Getting the relevant GameObjects
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();

        //2º:   Check if the game has started
        //      (if not, create the keys in PlayerPrefs, with the initial values)
        Debug.Log("Does startedGame exist? "+ PlayerPrefs.HasKey("startedGame"));
        Debug.Log("The value of startedGame is " +  PlayerPrefs.GetString("startedGame",""));
        if(!PlayerPrefs.HasKey("startedGame") || PlayerPrefs.GetString("startedGame","")!="true")
        {
            Debug.Log("Setting startedGame and points");
            PlayerPrefs.SetString("startedGame","true");
            PlayerPrefs.SetInt("points",0);
        }
        else
        {
            Debug.Log("Load moment according to SetUpScene");
            setUp.LoadMoment();
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

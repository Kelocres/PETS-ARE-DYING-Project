using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /*
    LIST OF SCENES:
    0 -> MCHouseScene
    1 -> StreetScene
    
    */

    //public int numberNextScene;
    public string nameNextScene;
    //Button button;
    GameObject button;

    void Start()
    {
        button = GameObject.FindGameObjectWithTag("ButtonInteract");
        button.GetComponent<Button>().onClick.AddListener(LoadNextScene);

        UnableButton();
    }

    public void LoadNextScene()
    {
        if(nameNextScene!=null)     SceneManager.LoadScene(nameNextScene);
    }

    public void SetUpButton(string newNameScene)
    {
        nameNextScene = newNameScene;
        button.SetActive(true);
    }

    public void UnableButton()
    {
        nameNextScene = "";
        button.SetActive(false);
    }
}

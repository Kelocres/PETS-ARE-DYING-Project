using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractScript : MonoBehaviour
{
    /*
    LIST OF SCENES:
    0 -> MCHouseScene
    1 -> StreetScene
    
    */

    //public int numberNextScene;
    public string nameNextScene;
    private Dialog nextDialog;
    private TriggerDialog trigger;
    //Button button;
    GameObject button;

    void Start()
    {
        button = GameObject.FindGameObjectWithTag("ButtonInteract");
        UnableButton();
    }

    public void LoadNextScene()
    {
        if(nameNextScene!=null)     
        {
            FindObjectOfType<LoadingSceneManager>().SaveBeforeNextScene();
            SceneManager.LoadScene(nameNextScene);
        }
    }

    public void LoadNextDialog()
    {
        if(nextDialog!=null)
        {
            if(trigger!=null)   trigger.Loaded();
            FindObjectOfType<DialogManager>().StartDialog(nextDialog);
            UnableButton();
        }
    }

    public void SetUpButtonForNextScene(string newNameScene, string information)
    {
        nameNextScene = newNameScene;
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text =information;
        button.GetComponent<Button>().onClick.RemoveListener(LoadNextDialog);
        button.GetComponent<Button>().onClick.AddListener(LoadNextScene);
    }

    public void SetUpButtonForDialog(Dialog dialog, string information)
    {
        nextDialog = dialog;
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text =information;
        button.GetComponent<Button>().onClick.RemoveListener(LoadNextScene);
        button.GetComponent<Button>().onClick.AddListener(LoadNextDialog);

    }

    public void SetUpButtonForDialog(TriggerDialog newTrigger)
    {
        trigger = newTrigger;
        nextDialog = trigger.dialog;
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = trigger.information;
        button.GetComponent<Button>().onClick.RemoveListener(LoadNextScene);
        button.GetComponent<Button>().onClick.AddListener(LoadNextDialog);

    }

    public void UnableButton()
    {
        trigger = null;
        nameNextScene = "";
        nextDialog = null;
        button.SetActive(false);
        button.GetComponent<Button>().onClick.RemoveListener(LoadNextDialog);
        button.GetComponent<Button>().onClick.RemoveListener(LoadNextScene);
    }
}

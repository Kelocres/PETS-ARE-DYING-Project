using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    DialogManager dmanager;
    Button button;
    Text textButton;
    private OptionDialog optionDialog;
    //private int pointsOption;

    //void Start()
    public void StartOptionButton()
    {
        button = GetComponent<Button>();
        textButton = button.GetComponentInChildren<Text>();
        dmanager = FindObjectOfType<DialogManager>();
        //Debug.Log("Button started");
    }

    public void LinkOption(OptionDialog od)
    {
        gameObject.SetActive(true);
        optionDialog = od;
        textButton.text = optionDialog.showOption;
        //pointsOption = optionDialog.points;

        button.onClick.AddListener(OnClickOption);
    }

    public void OnClickOption()
    {
        dmanager.OnClickOption(optionDialog);
    }

    public void HideButton()
    {
        gameObject.SetActive(false);
        button.onClick.RemoveListener(OnClickOption);
        //button.onClick.RemoveAllListeners();
        //pointsOption = 0;
        optionDialog = null;
    }
}

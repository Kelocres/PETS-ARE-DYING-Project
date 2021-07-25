using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    //Arrays of game elements
    //public DecisionScript [] listDecision;
    //public OptionScript [] listOption;
    public Texture [] listBackgroundImages;
    public Animator animDialogBox;
    private Queue<DialogLine> linesForDialog;

    public Text txtName;
    public Text txtDialog;

    //Canvas element in which put the background images
    public RawImage raw_bgImage;
    //In order to control the alpha (transparency) of the background image
    public CanvasGroup cg_bgImage;

    //The buttons for the options in a decision
    private Button optionA;
    private Button optionB;
    //The OptionDialogs that are triggered 
    private OptionDialog optionDialogA;
    private OptionDialog optionDialogB;

    //Just for trying the code during this firsts days
    public Dialog testingDialog;

    private Dialog currentDialog;

    //Coroutine for writing the text
    private IEnumerator dialogWriter;
    private IEnumerator dialogBGImage;

    // Start is called before the first frame update
    void Start()
    {
        //FindGameObjectWithTag doesn't work with UI.Image
        //canvas_bgImage = GameObject.FindGameObjectWithTag("BackgroundImage");
        GameObject getObject = GameObject.FindGameObjectWithTag("BackgroundImage");
        if(getObject!=null)
        {
             raw_bgImage = getObject.GetComponent<RawImage>();
             cg_bgImage = getObject.GetComponent<CanvasGroup>();
             cg_bgImage.alpha = 0f;
        }

        //Find the bottons for the options
        GameObject[] getListObjects = GameObject.FindGameObjectsWithTag("ButtonOption");
        Debug.Log("ButtonOptions detected: "+getListObjects.Length);
        if(getListObjects.Length == 2)
        {
            optionA = getListObjects[0].GetComponent<Button>();
            optionB = getListObjects[1].GetComponent<Button>();
            Debug.Log("ButtonOptions detected");
        }

        //Create an empty queue
        linesForDialog = new Queue<DialogLine>();

        //Start the prototype
        if(testingDialog!=null)     StartDialog(testingDialog);
    }

    public void StartDialog(Dialog getDialog)
    {
        //Debug.Log("New conversation");
        currentDialog = getDialog;

        animDialogBox.SetBool("isOpen", true);

        if(currentDialog.selectBGImage != -1)
        {
            Debug.Log("Let's show a background image");
            if(dialogBGImage!=null) StopCoroutine(dialogBGImage);
            dialogBGImage = FadeInBackgroundImage(currentDialog.selectBGImage);
            StartCoroutine(dialogBGImage);
        }

        //Eliminate all lines remaining in linesForDialog (from previous dialogs)
        linesForDialog.Clear();

        //Put lines from currentDialog in the queue
        foreach(DialogLine line in currentDialog.dialogLines)
            linesForDialog.Enqueue(line);

        ShowCurrentLine();
    }

    public void ShowCurrentLine()
    {
        
        if(linesForDialog.Count == 0)
        {
            Debug.Log("End of DialogLines");
            //In case that there is a decision
            if(currentDialog.decision == null || currentDialog.finish_dialog)
            {
                FinishDialog();
                return;
            }
            else
            {
                Debug.Log("Let's take a decision");
                //setBool of isDecision
                animDialogBox.SetBool("isDecision", true);
                //Read text from DecisonScript like a DialogLine
                DecisionLine decision = currentDialog.decision;
                txtName.text = decision.name;
                
                if(dialogWriter!=null) StopCoroutine(dialogWriter);
                dialogWriter = WriteLine(decision.text);
                StartCoroutine(dialogWriter);

                //Link CANVAS bottons to the options of the decision

                optionA.GetComponentInChildren<Text>().text = decision.optionA.showOption;
                //Assing function of decision.optionA
                optionA.onClick.AddListener(OnClickOptionA);
                optionDialogA = decision.optionA;

                optionB.GetComponentInChildren<Text>().text = decision.optionB.showOption;
                //Assing function of decision.optionB
                optionB.onClick.AddListener(OnClickOptionB);
                optionDialogB = decision.optionB;

                //The amount of bottons in the screen is related to the 
                //amount of options of the decisions (array's length)
                return;
            }
            
        }
        //Debug.Log("Showing the current line");

        DialogLine currentLine = linesForDialog.Dequeue();

        //Show name of the speaker
        txtName.text = currentLine.name;

        //Begin to write the line in the DialogBox
        //StopAllCoroutines();
        if(dialogWriter!=null) StopCoroutine(dialogWriter);
        dialogWriter = WriteLine(currentLine.text);
        StartCoroutine(dialogWriter);

    }

    public void OnClickOptionA()
    {
        Debug.Log("Option A is chosen");
        ChangeDialog(optionDialogA);
    }

    public void OnClickOptionB()
    {
        Debug.Log("Option B is chosen");
        ChangeDialog(optionDialogB);
    }

    public void ChangeDialog(OptionDialog chosenOption)
    {
        //Disable the ButtonOptions
        optionA.onClick.RemoveListener(OnClickOptionA);
        optionB.onClick.RemoveListener(OnClickOptionB);

        //Change isDecision in animBoxDialog
        animDialogBox.SetBool("isDecision",false);

        StartDialog(chosenOption.newDialog);

    }

    IEnumerator WriteLine(string line)
    {
        //Debug.Log("New routine for writting lines");
        txtDialog.text = "";
        foreach(char letter in line.ToCharArray())
        {
            txtDialog.text += letter;
            yield return null;
        }
    }

    IEnumerator FadeInBackgroundImage(int select)
    {
        raw_bgImage.texture = listBackgroundImages[select];
        while(cg_bgImage.alpha < 1f)
        {
            cg_bgImage.alpha += 1f * Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator FadeOutBackgroundImage()
    {
        while(cg_bgImage.alpha > 0f)
        {
            cg_bgImage.alpha -= 1f * Time.deltaTime;
            yield return null;
        }
    }

    public void FinishDialog()
    {
        animDialogBox.SetBool("isOpen", false);

        //Stop showing the background image (if there is any)
        if(currentDialog.selectBGImage != -1)
        {
            StopCoroutine(dialogBGImage);
            dialogBGImage = FadeOutBackgroundImage();
            StartCoroutine(dialogBGImage);
        }

        //Communicate to the player script that the controls are able now

    }
}
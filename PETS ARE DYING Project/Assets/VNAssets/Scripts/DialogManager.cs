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
    private bool showing_bgImage;

    //When the image background has to transitionate from one texture to another
    public RawImage raw_bgImageLerp;
    public CanvasGroup cg_bgImageLerp;

    //The buttons for the options in a decision
    private Button optionA;
    private Button optionB;
    //The OptionDialogs that are triggered 
    private OptionDialog optionDialogA;
    private OptionDialog optionDialogB;

    //Just for trying the code during this firsts days
    //public Dialog testingDialog;

    private Dialog currentDialog;

    //Coroutine for writing the text
    private IEnumerator dialogWriter;
    private IEnumerator dialogBGImage;

    //public enum ActionDialog : short {Finish = 0, Decision = 1, NextDialog = 2};

    private PlayerMovement2 player;

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
             showing_bgImage = false;
        }
        getObject = GameObject.FindGameObjectWithTag("BackgroundImageLerp");
        if(getObject!=null)
        {
             raw_bgImageLerp = getObject.GetComponent<RawImage>();
             cg_bgImageLerp = getObject.GetComponent<CanvasGroup>();
             cg_bgImage.alpha = 0f;
        }

        txtName = GameObject.FindGameObjectWithTag("DialogBoxName").GetComponent<Text>();
        txtDialog = GameObject.FindGameObjectWithTag("DialogBoxText").GetComponent<Text>();
        
        //THIS DOESN'T WORK, IT MUST BE DONE IN THE INSPECTOR
        //GameObject.FindGameObjectWithTag("ButtonNextDialog").GetComponent<Button>().onClick.AddListener(delegate {ShowCurrentLine();});

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

        player = FindObjectOfType<PlayerMovement2>();

        //Start the prototype
        //if(testingDialog!=null)     StartDialog(testingDialog);
    }

    public void StartDialog(Dialog getDialog)
    {
        //Debug.Log("New conversation");
        currentDialog = getDialog;

        player.ableToWalk = false;

        animDialogBox.SetBool("isOpen", true);

        if(currentDialog.selectBGImage != -1 && !showing_bgImage)
        {
            Debug.Log("Let's show a background image");
            showing_bgImage = true;
            if(dialogBGImage!=null) StopCoroutine(dialogBGImage);
            dialogBGImage = FadeInBackgroundImage(currentDialog.selectBGImage);
            StartCoroutine(dialogBGImage);
        }
        else if(currentDialog.selectBGImage == -1 && showing_bgImage)
        {
            showing_bgImage = false;
            if(dialogBGImage!=null) StopCoroutine(dialogBGImage);
            dialogBGImage = FadeOutBackgroundImage();
            StartCoroutine(dialogBGImage);
        }
        else if(currentDialog.selectBGImage != -1 && showing_bgImage)
        {
            if(dialogBGImage!=null) StopCoroutine(dialogBGImage);
            dialogBGImage = ChangeBackgroundImage(currentDialog.selectBGImage);
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
            //if(currentDialog.decision == null || currentDialog.finish_dialog)
            if(currentDialog.afterDialogIsNextDialog())
            {
                ChangeDialog(currentDialog.nextDialog);
                return;
            }
            else if(currentDialog.afterDialogIsDecison())
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
            else if(currentDialog.afterDialogIsEvent())
            {
                Debug.Log("An event is gonna happen");
                currentDialog.ade.Activate();
                FinishDialog();
                return;
            }
            //if(currentDialog.afterDialogIsFinish())
            else
            {
                FinishDialog();
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
        //ChangeDialog(optionDialogA);
        ChangeDialog(optionDialogA.newDialog);
    }

    public void OnClickOptionB()
    {
        Debug.Log("Option B is chosen");
        //ChangeDialog(optionDialogB);
        ChangeDialog(optionDialogB.newDialog);
    }

    public void ChangeDialog(Dialog chosenOption)
    {
        //Disable the ButtonOptions
        optionA.onClick.RemoveListener(OnClickOptionA);
        optionB.onClick.RemoveListener(OnClickOptionB);

        //Change isDecision in animBoxDialog
        animDialogBox.SetBool("isDecision",false);

        StartDialog(chosenOption);

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

    IEnumerator ChangeBackgroundImage(int select)
    {
        raw_bgImageLerp.texture = listBackgroundImages[select];
        while(cg_bgImageLerp.alpha < 1f)
        {
            cg_bgImageLerp.alpha += 1f * Time.deltaTime;
            yield return null;
        }
        //Change in a frame the background image texture 
        raw_bgImage.texture = listBackgroundImages[select];
        cg_bgImageLerp.alpha = 0f;

    }

    public void FinishDialog()
    {
        animDialogBox.SetBool("isOpen", false);
        player.ableToWalk = true;

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

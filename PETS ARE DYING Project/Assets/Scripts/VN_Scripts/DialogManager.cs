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

    /*//The buttons for the options in a decision
    private Button optionA;
    private Button optionB;
    //The OptionDialogs that are triggered 
    private OptionDialog optionDialogA;
    private OptionDialog optionDialogB;
    //The points of the options
    private int pointsOptionA;
    private int pointsOptionB;*/

    //private Button [] options;
    //private OptionDialog [] optionsDialog;
    //private int [] numSelect;
    //private int [] pointsOption;
    private OptionButton [] optionButtons;

    //The scene begins with this dialog
    public Dialog startingDialog;
    public bool doStartingDialog = false;

    private Dialog currentDialog;

    //Coroutine for writing the text
    private IEnumerator dialogWriter;
    private IEnumerator dialogBGImage;

    //public enum ActionDialog : short {Finish = 0, Decision = 1, NextDialog = 2};

    private PlayerMovement2 player;
    private PlayerData playerData;

    // Start is called before the first frame update
    //void Start()
    public void StartDialogManager()
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

        animDialogBox = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<Animator>();
        txtName = GameObject.FindGameObjectWithTag("DialogBoxName").GetComponent<Text>();
        txtDialog = GameObject.FindGameObjectWithTag("DialogBoxText").GetComponent<Text>();
        
        //THIS DOESN'T WORK, IT MUST BE DONE IN THE INSPECTOR
        //GameObject.FindGameObjectWithTag("ButtonNextDialog").GetComponent<Button>().onClick.AddListener(delegate {ShowCurrentLine();});

        //Find the bottons for the options
        /*GameObject[] getListObjects = GameObject.FindGameObjectsWithTag("ButtonOption");
        Debug.Log("ButtonOptions detected: "+getListObjects.Length);
        if(getListObjects.Length == 2)
        {
            optionA = getListObjects[0].GetComponent<Button>();
            optionB = getListObjects[1].GetComponent<Button>();
            Debug.Log("ButtonOptions detected");
        }*/

        //Get the Option Buttons for the decisions
        optionsContainerScript container = GameObject.FindGameObjectWithTag("optionsContainer").GetComponent<optionsContainerScript>();
        optionButtons = container.optionButtons;
        //pointsOption = new int[options.Length];
        //numSelect = new int[options.Length];
        //optionsDialog = new OptionDialog[options.Length];
        //Debug.Log("Length of options array: "+options.Length);
        for(int i=0; i<optionButtons.Length; i++)   
            optionButtons[i].StartOptionButton();
        HideOptionButtons();


        //Create an empty queue
        linesForDialog = new Queue<DialogLine>();
        player = FindObjectOfType<PlayerMovement2>();
        playerData = FindObjectOfType<PlayerData>();
        //pointsOptionA = 0;
        //pointsOptionB = 0;

        //Launch startingDialog
        if(doStartingDialog)     StartDialog(startingDialog);
    }

    void HideOptionButtons()
    {
        for(int i=0; i<optionButtons.Length; i++)
        {
            //options[i].gameObject.SetActive(false);
            //options[i].onClick.RemoveListener(() => OnClickOption(i));
            //options[i].onClick.RemoveAllListeners();
            //pointsOption[i] = 0;

            optionButtons[i].HideButton();
        }
    }

    public void StartDialog(Dialog getDialog)
    {
        //Debug.Log("New conversation");
        currentDialog = getDialog;
        if(currentDialog==null)
        {
            FinishDialog();
            return;
        }
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
            //Debug.Log("End of DialogLines");
            //In case that there is a decision
            //if(currentDialog.decision == null || currentDialog.finish_dialog)
            if(currentDialog.afterDialogIsNextDialog())
            {
                ChangeDialog(currentDialog.nextDialog);
                return;
            }
            else if(currentDialog.afterDialogIsDecison())
            {
                //Debug.Log("Let's take a decision");
                //setBool of isDecision
                animDialogBox.SetBool("isDecision", true);
                //Read text from DecisonScript like a DialogLine
                DecisionLine decision = currentDialog.decision;
                txtName.text = decision.name;
                
                if(dialogWriter!=null) StopCoroutine(dialogWriter);
                dialogWriter = WriteLine(decision.text);
                StartCoroutine(dialogWriter);

                //Link CANVAS bottons to the options of the decision

                /*optionA.GetComponentInChildren<Text>().text = decision.optionA.showOption;
                //Assing function of decision.optionA
                optionA.onClick.AddListener(OnClickOptionA);
                optionDialogA = decision.optionA;
                if(optionDialogA.points!=0)     pointsOptionA = optionDialogA.points;

                optionB.GetComponentInChildren<Text>().text = decision.optionB.showOption;
                //Assing function of decision.optionB
                optionB.onClick.AddListener(OnClickOptionB);
                optionDialogB = decision.optionB;
                if(optionDialogB.points!=0)     pointsOptionB = optionDialogB.points;
                */

                int numberOptions = decision.options.Length;
                if(numberOptions==0)
                {
                    //There are not options, so the Dialog ends
                    FinishDialog();
                    return;
                }

                for(int i=0; i<numberOptions; i++)
                {
                    /*options[i].gameObject.SetActive(true);
                    options[i].GetComponentInChildren<Text>().text = decision.options[i].showOption;
                    optionsDialog[i] = decision.options[i];
                    pointsOption[i] = optionsDialog[i].points;
                    numSelect[i] = i;
                    
                    //Assing function of decision.options[i]
                    options[i].onClick.AddListener(() => OnClickOption(numSelect[i]));  
                    Debug.Log("Creating Button " +numSelect[i]); */ 
                    optionButtons[i].LinkOption(decision.options[i]);                
                }
                //The amount of bottons in the screen is related to the 
                //amount of options of the decisions (array's length)
                return;
            }
            else if(currentDialog.afterDialogIsConditionedDecision())
            {
                Debug.Log("Let's take a conditioned decision");
                //Get the decision
                ConditionedDecision conditionedDecision = currentDialog.conditionedDecision;
                conditionedDecision.TryOptions();
                //See if there is any option unlocked
                if(conditionedDecision.options[0]==null)
                {
                    Debug.Log("There are no unlocked options, so the dialog is over");
                    FinishDialog();
                    return;
                }

                //Set the Decision Line and the options
                animDialogBox.SetBool("isDecision", true);
                txtName.text = conditionedDecision.name;
                
                if(dialogWriter!=null) StopCoroutine(dialogWriter);
                dialogWriter = WriteLine(conditionedDecision.text);
                StartCoroutine(dialogWriter);

                //optionButtons = conditionedDecision.options;
                int j = 0;
                
                for(int i=0; i<conditionedDecision.options.Length; i++)
                    if(conditionedDecision.options[i]!=null)
                        optionButtons[j++].LinkOption(conditionedDecision.options[i]);                
                
                //The amount of bottons in the screen is related to the 
                //amount of options of the decisions (array's length)
                return;

            }
            else if(currentDialog.afterDialogIsEvent())
            {
                //Debug.Log("An event is gonna happen");
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

    /*public void OnClickOptionA()
    {
        //Debug.Log("Option A is chosen");
        playerData.PointsDuringDialog(pointsOptionA);
        //ChangeDialog(optionDialogA);
        ChangeDialog(optionDialogA.newDialog);
    }

    public void OnClickOptionB()
    {
        //Debug.Log("Option B is chosen");
        playerData.PointsDuringDialog(pointsOptionB);
        //ChangeDialog(optionDialogB);
        ChangeDialog(optionDialogB.newDialog);
    }*/

    public void OnClickOption(OptionDialog od)
    {
        //Debug.Log("Button "+select+" selected");
        //playerData.PointsDuringDialog(pointsOption[select]);
        //ChangeDialog(optionsDialog[select].newDialog);
        playerData.PointsDuringDialog(od.points);
        ChangeDialog(od.newDialog);
    }

    public void ChangeDialog(Dialog chosenOption)
    {
        //Disable the ButtonOptions
        //optionA.onClick.RemoveListener(OnClickOptionA);
        //optionB.onClick.RemoveListener(OnClickOptionB);

        //Reset the values of points
        //pointsOptionA = 0;
        //pointsOptionB = 0;
        /*for(int i=0; i<options.Length; i++)
        {
            options[i].onClick.RemoveListener(() => OnClickOption(i));
            pointsOption[i] = 0;
        }*/
        HideOptionButtons();


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
        Debug.Log("End of the Dialog");
        animDialogBox.SetBool("isOpen", false);
        player.ableToWalk = true;

        

        //Stop showing the background image (if there is any)
        //if(currentDialog!=null && currentDialog.selectBGImage != -1)
        //{
            Debug.Log("Fade out the background image");
            //StopCoroutine(dialogBGImage);
            dialogBGImage = FadeOutBackgroundImage();
            StartCoroutine(dialogBGImage);
        //}

        //Communicate to the player script that the controls are able now

    }
}

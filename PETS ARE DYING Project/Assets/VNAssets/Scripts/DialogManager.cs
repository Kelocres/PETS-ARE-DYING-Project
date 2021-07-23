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

    //Just for trying the code during this firsts days
    public Dialog testingDialog;

    private Dialog currentDialog;

    //Coroutine for writing the text
    private IEnumerator dialogWritter;
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

        //Create an empty queue
        linesForDialog = new Queue<DialogLine>();

        //Start the prototype
        StartDialog(testingDialog);
    }

    public void StartDialog(Dialog getDialog)
    {
        //Debug.Log("New conversation");
        currentDialog = getDialog;

        animDialogBox.SetBool("isOpen", true);

        if(currentDialog.selectBGImage != -1)
        {
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
            //In case that there is a decision
            if(currentDialog.decision != null)
            {
                //setBool of isDecision
                //Read text from DecisonScript like a DialogLine
                //Link CANVAS bottons to the options of the decision
                //The amount of bottons in the screen is related to the 
                //amount of options of the decisions (array's length)
            }
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
        StartCoroutine(WriteLine(currentLine.text));

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

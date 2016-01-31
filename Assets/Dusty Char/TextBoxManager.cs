using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox; //This is the "panel" that is needed for the "dialog box"
    public Text theText;        //This is the "text" section of the Panel box, for rendering text

    public TextAsset textfile;  //Whichever textfile that is needed to be "rendered" on the screen.
    public string[] textLines;

    public int currentLine;
    public int endAtLine;
    public PlayerCharacter player;

    public bool isActive;
    public bool stopPlayerMovement;
    


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>(); //What ever you are using is what you should be "finding"

        if (textfile != null)       
            textLines = (textfile.text.Split('\n'));
        
        if (endAtLine == 0)        
            endAtLine = textLines.Length - 1;       

        if(isActive)
            EnableTextBox();
        
        else
            DisableTextBox();
        

    }

    void Update()
    {
        if (!isActive)
            return;

        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.K)) 
            currentLine++;

        if (currentLine > endAtLine)
            DisableTextBox();
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        if (stopPlayerMovement)
            player.canMove = false;
       
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);   //Once we get to the end of the "lines" we turn off the text block.
        player.canMove = true;
        isActive = false;
    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null) //making sure there is a file being passed in.
        {
            textLines = new string[1];  //take the array that already exists, remove it, and starts to fill it in with a new one....
            textLines = (theText.text.Split('\n'));
        }
            
    }
}

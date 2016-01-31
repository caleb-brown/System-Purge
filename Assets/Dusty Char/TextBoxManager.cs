using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public Text theText;

    public TextAsset textfile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;
    public PlatformerCharacter2D player;

    public bool isActive;
    public bool stopPlayerMovement;
    


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlatformerCharacter2D>();

        if (textfile != null)
        {
            textLines = (textfile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if(isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

    }

    void Update()
    {
        if (!isActive)
            return;

        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.K)) 
            currentLine++;

        if (currentLine > endAtLine)
        {
            DisableTextBox();
            
        }
            
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        if (stopPlayerMovement)
        {
            player.canMove = false;
        }
            
       
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);   //Once we get to the end of the "lines" we turn off the text block.
        isActive = false;
        player.canMove = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        //So we can use different scripts within the game
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
    }
}

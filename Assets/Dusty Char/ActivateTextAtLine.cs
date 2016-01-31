using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;
    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;


    public bool requireButtonPress;
    private bool waitForPressed;
    public bool destroyWhenActivated;

	// Use this for initialization
	void Start () {
        theTextBox = FindObjectOfType<TextBoxManager>();	
	}
	
	// Update is called once per frame
	void Update () {

        if (waitForPressed && Input.GetKeyDown(KeyCode.J))
        {
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
                Destroy(gameObject);
        }
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("player") ) //make sure it is what the "main" character is of.
        {
            if(requireButtonPress)
            {
                waitForPressed = true;
                return;
            }

            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
                Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            waitForPressed = false; //checking when the "player" leaves the "bounding" area for the speech to take place.
    }
}

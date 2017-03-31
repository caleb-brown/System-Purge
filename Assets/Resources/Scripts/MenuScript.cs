using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas creditsMenu;
    public Button startText;
    public Button exitText;
    public Button creditText;



	// Use this for initialization
	void Start () {

        Debug.Log("biiiiiitch");
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        creditsMenu = creditsMenu.GetComponent<Canvas>();

        quitMenu.enabled = false;
        creditsMenu.enabled = false;
	}


    public void ExitPress()  //Pressing the Quit Menu option
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
        creditsMenu.enabled = false;
        creditText.enabled = false;
    }

    public void CreditsPress()  //Pressing the Credits Menu option
    {
        creditsMenu.enabled = true;
        quitMenu.enabled = false;
        startText.enabled = false;
        exitText.enabled = false;
        creditText.enabled = false;
    }
	
    public void NoPress()
    {
        quitMenu.enabled = false;
        creditsMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
        creditText.enabled = true;
    }


    public void StartLevel()
    {
        SceneManager.LoadScene(1);
        Debug.Log("bitch");

    }

    public void ExitGame()
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {
	
	}
}

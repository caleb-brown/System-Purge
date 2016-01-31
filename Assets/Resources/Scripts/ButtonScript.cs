using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public string nextLevel;
    bool isTriggered = false;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            GameManager.gManager.SetGameStateAndLoad("Level2");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            GameManager.gManager.SetGameStateAndLoad("Level3");
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            GameManager.gManager.SetGameStateAndLoad("Level4");
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {
            Application.Quit();
        }
        if (other.gameObject.tag == "Scene_Object")
        {
            isTriggered = true;
            SceneManager.LoadScene(nextLevel);
        }
    }

    void Update()
    {
        /*if (isTriggered)
        {
            foreach(Transform i in this.transform)
            {
                if (i.tag == "Button")
                {
                    i.transform.position = Vector3.Lerp(i.transform.position, new Vector3(0, 0, 0), Time.deltaTime);
                }
            }
        }*/
    }
}

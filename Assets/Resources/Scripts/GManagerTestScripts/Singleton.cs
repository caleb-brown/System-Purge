using System;
using UnityEngine;
using UnityStandardAssets._2D;
using System.Collections;

public class Singleton : MonoBehaviour {

    private GameManager gManager;

	void Awake()
    {
        gManager = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<GameManager>();
        if(GameManager.gManager == null && gameObject.tag == "Game_Manager")
        {
            print("Assigning game manager.");
            GameManager.gManager = gameObject.GetComponent<GameManager>();
            DontDestroyOnLoad(gameObject);
        }
        else if(GameManager.m_Character == null && gameObject.layer == LayerMask.NameToLayer("player"))
        {
            print("Assigning main character");
            GameManager.m_Character = gameObject.GetComponent<PlatformerCharacter2D>();
            DontDestroyOnLoad(gameObject);
        }
        else if(GameManager.m_Camera == null && gameObject.tag == "MainCamera")
        {
            print("Assigning main camera.");
            GameManager.m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            print("FOUND ANOTHER INSTANCE OF SINGLETON");
            Destroy(gameObject);
        }
    }
}

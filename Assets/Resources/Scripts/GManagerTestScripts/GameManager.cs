using System;
using System.Linq;
using UnityEngine;
using GameUtils;
using UnityStandardAssets._2D;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gManager; // gManager for GameManager, iManager for InputManager, etc...
    public static InputManager iManager;
    public static PlatformerCharacter2D m_Character;
    public static Camera m_Camera;
    public static GameState gState;

    public Dictionary<GameState, string> levelDictionary;

    private GameObject[] tagged_objects;
    private ISceneObject[] scene_objects;
    private string _testLevelPrefix, _regularLevelPrefix;

    void Start()
    {
        if (gManager == this)
        {
            iManager = new InputManager(0.1f, 0.2f); // It could be helpful, I guess.
            tagged_objects = GameObject.FindGameObjectsWithTag("Scene_Object");
            scene_objects = new ISceneObject[tagged_objects.Length];
            iManager.Initialize();
            // Initialize the list of scene objects, all of which have ONE ISceneObject component.
            for (int i = 0; i < tagged_objects.Length; i++)
            {
                scene_objects[i] = tagged_objects[i].GetComponent<ISceneObject>(); // Grab all of those scene objects!
            }

            // Initialize all scene objects.
            for (int j = 0; j < scene_objects.Length; j++)
            {
                scene_objects[j].Initialize();
            }
            levelDictionary = new Dictionary<GameState, string>() { { GameState.TLEVELONE, "GMLevel1" },
                                                                    { GameState.TLEVELTWO, "GMLevel2" } };
            // This will break on regular levels, handle regular levels separately.
            GameManager.gState = levelDictionary.FirstOrDefault(x => x.Value == _testLevelPrefix + SceneManager.GetActiveScene().name).Key;
        }
    }

    public IEnumerator SetGameStateAndLoad(string levelName)
    {
        GameManager.gState = levelDictionary.FirstOrDefault(x => x.Value == levelName).Key;
        // Trigger Loading Screen
        transform.Find("LoadingScreen").gameObject.SetActive(true);
        AsyncOperation aSyncOp = SceneManager.LoadSceneAsync(levelDictionary[GameManager.gState]);
        aSyncOp.allowSceneActivation = true;
        float progress = 0.0f;
        while (progress < 0.9f) // whatever "100" is
        {
            progress = aSyncOp.progress * 100.0f;
            print("Loading: " + progress);
            yield return null;
        }
        transform.Find("LoadingScreen").gameObject.SetActive(false);
        yield break;
    }

    /// <summary>
    /// Update all scene objects in one update loop.
    /// </summary>
    void Update()
    {
        iManager.ObjectUpdate();
        for (int j = 0; j < scene_objects.Length; j++)
        {
            scene_objects[j].ObjectUpdate();
        }
        if (GameManager.gManager != null && GameManager.iManager != null && GameManager.m_Camera != null)
            return;
        if (GameManager.gManager == null)
            throw new Exception("We did not assign the game manager.");
        if (GameManager.iManager == null)
            throw new Exception("We did not assign an input manager.");
        if (GameManager.m_Camera == null)
            throw new Exception("We did not assign a main camera.");
        // It can be the case that there is no character in the scene (i.e. main menu) so we won't check for that.
    }
}
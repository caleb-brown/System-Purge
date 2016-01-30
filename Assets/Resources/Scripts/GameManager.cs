using UnityEngine;
using System.Collections;
using GameUtils;

public class GameManager : MonoBehaviour
{

    public static GameManager gManager; // gManager for GameManager, iManager for InputManager, etc...
    public static InputManager iManager;

    private GameObject[] tagged_objects;
    private ISceneObject[] scene_objects;

    void Start()
    {
        if (gManager == null)
        {
            DontDestroyOnLoad(gameObject);
            gManager = this;
            iManager = new InputManager(0.1f, 0.2f);
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
        }
        else
        {
            Destroy(gameObject);
        }
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
    }
}
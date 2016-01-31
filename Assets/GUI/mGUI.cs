using UnityEngine;
using System.Collections;

public class mGUI : MonoBehaviour
{
    public GUISkin mySkin;
    public float curHealth = 100;
    public float maxHealth = 100;
    public Texture2D bgImage;
    public Texture2D fgImage;
    float nativeWidth = 640;
    float nativeHeight = 400;
    float percentHealth;
    Matrix4x4 guiMatrix;

    void Start()
    {
        Vector3 scale = new Vector3(.5f, .5f, 1.0f);
        print(scale);
        guiMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
    }


    Matrix4x4 GetGUIMatrix()
    {
        return guiMatrix;
    }



    void OnGUI()
    {
        GUI.matrix = GetGUIMatrix();
        // Create one Group to contain both images
        // Adjust the first 2 coordinates to place it somewhere else on-screen
        GUI.BeginGroup(new Rect(0, 0, 207, 82));

        // Draw the background image
        GUI.backgroundColor = new Color(0, 0, 0, 0.0f);
        GUI.Box(new Rect(0, 0, 207, 82), bgImage);
       
        percentHealth = curHealth / maxHealth;
        // Create a second Group which will be clipped
        // We want to clip the image and not scale it, which is why we need the second Group
        GUI.BeginGroup(new Rect(0, 0, percentHealth * 207, 82));

        // Draw the foreground image
        GUI.backgroundColor = new Color(0, 0, 0, 0.0f);
        GUI.Box(new Rect(0, 0, 207, 82), fgImage);
       

        // End both Groups
        GUI.EndGroup();

        GUI.EndGroup();
    }
}

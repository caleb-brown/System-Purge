using UnityEngine;
using System.Collections;

public class GlitchPlatform : MonoBehaviour
{
    public float time = 5.0f;
    float second;
	
	// Update is called once per frame
	void Update ()
    {
        //Keeps count of the seconds
        second += Time.deltaTime * 1;

        if (second <= time)
        {
            //Turn off colliders of the children GameObjects
            foreach (Collider2D i in GetComponentsInChildren<Collider2D>())
            {
                i.enabled = false;
            }
            foreach (SpriteRenderer i in GetComponentsInChildren<SpriteRenderer>())
            {
                i.enabled = false;
            }
        }
        
        if (second >= time)
        {
            //Turn on colliders of the children GameObjects
            foreach (Collider2D i in GetComponentsInChildren<Collider2D>())
            {
                i.enabled = true;
            }
            foreach (SpriteRenderer i in GetComponentsInChildren<SpriteRenderer>())
            {
                i.enabled = true;
            }
        }
        
        //Reset second counter
        if (second >= time * 2)
            second = 0.0f;
    }
}

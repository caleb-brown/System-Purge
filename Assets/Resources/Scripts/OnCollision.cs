using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            print("Ow!");
            // player is damaged

        }
    }
}



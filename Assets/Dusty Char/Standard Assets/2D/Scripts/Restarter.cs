using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        public string levelName;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Scene_Object")
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}

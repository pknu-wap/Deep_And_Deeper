using System;
using Hero;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapGenerator
{
    public class Hole : MonoBehaviour
    {
        public int nextStage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            HeroManager.Instance.SetStage(nextStage);
            MapGenerator.Instance.needUpdate = true;
            MapGenerator.Instance.savedPosition = Vector3.zero;
            MapGenerator.Instance.saved = false;
            MapGenerator.Instance.savedCameraPosition = new Vector3(0, 0, -10);
            SceneManager.LoadScene("RandomMapTest");
        }
    }
}
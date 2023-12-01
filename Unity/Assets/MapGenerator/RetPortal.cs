using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapGenerator
{
    public class RetPortal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene("RandomMapTest");
        }
    }
}
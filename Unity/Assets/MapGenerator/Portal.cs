using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapGenerator
{
    public class Portal : MonoBehaviour
    {
        public int y;
        public int x;

        private void OnTriggerEnter2D(Collider2D other)
        {
            MapGenerator.Instance.Clear(y, x);

            SceneManager.LoadScene(MapGenerator.Instance.GetRandomMap());
        }
    }
}
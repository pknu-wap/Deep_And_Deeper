using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapGenerator
{
    public class BossPortal : MonoBehaviour
    {
        public int y;
        public int x;
        private void OnTriggerEnter2D(Collider2D other)
        {
            MapGenerator.Instance.BossClear(y, x);
            SceneManager.LoadScene(MapGenerator.Instance.GetBossMap());
        }
    }
}
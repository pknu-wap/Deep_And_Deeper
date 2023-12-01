using UnityEngine;

namespace Audio
{
    public class SoundBox : MonoBehaviour
    {
        private void Awake()
        {
            if (GameObject.FindGameObjectsWithTag("SoundBox").Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
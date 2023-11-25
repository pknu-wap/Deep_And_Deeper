using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title
{
    public class Title : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private string sceneName;

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
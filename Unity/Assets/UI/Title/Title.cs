using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Title
{
    public class Title : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        private void Update()
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
using UnityEngine;

namespace Hero
{
    public class GroundChecker : MonoBehaviour
    {
        private void OnCollisionEnter2D()
        {
            HeroManager.Instance.SetGrounded(true);
        }

        private void OnCollisionExit2D()
        {
            HeroManager.Instance.SetGrounded(false);
        }
    }
}
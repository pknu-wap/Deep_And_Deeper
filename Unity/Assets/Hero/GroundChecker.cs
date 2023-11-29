using UnityEngine;

namespace Hero
{
    public class GroundChecker : MonoBehaviour
    {
        private void OnCollisionEnter2D()
        {
            HeroManager.Instance.isGrounded = true;
        }

        private void OnCollisionExit2D()
        {
            HeroManager.Instance.isGrounded = false;
        }
    }
}
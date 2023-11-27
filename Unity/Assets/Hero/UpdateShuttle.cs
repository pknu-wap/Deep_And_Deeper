using UnityEngine;

namespace Hero
{
    public class UpdateShuttle : MonoBehaviour
    {
        private void Update()
        {
            HeroManager.Instance.Update();
        }
    }
}
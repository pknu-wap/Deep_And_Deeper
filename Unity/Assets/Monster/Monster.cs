using UnityEngine;

namespace Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private float hp = 1;
        [SerializeField] private float speed = 1;

        private void Attack()
        {
            Debug.Log("attack!");
        }
    }
}
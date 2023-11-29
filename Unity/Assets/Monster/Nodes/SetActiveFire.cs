using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/SetActiveFire")]
    public class SetActiveFire : Leaf
    {
        [SerializeField] private bool active;
        private SpriteRenderer _fire;

        private void Start()
        {
            _fire = GameObject.FindGameObjectWithTag("Fire").GetComponent<SpriteRenderer>();
        }

        public override NodeResult Execute()
        {
            _fire.enabled = active;
            return NodeResult.success;
        }
    }
}
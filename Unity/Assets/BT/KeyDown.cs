using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/KeyDown")]
    public class KeyDown : Leaf
    {
        [SerializeField] private KeyCode keyCode;

        public override NodeResult Execute()
        {
            return Input.GetKeyDown(keyCode) ? NodeResult.success : NodeResult.failure;
        }
    }
}
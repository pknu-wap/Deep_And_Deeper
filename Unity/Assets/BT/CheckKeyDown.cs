using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/CheckKeyDown")]
    public class CheckKeyDown : Leaf
    {
        [SerializeField] private KeyCode keyCode;

        public override NodeResult Execute()
        {
            return Input.GetKeyDown(keyCode) ? NodeResult.success : NodeResult.failure;
        }
    }
}
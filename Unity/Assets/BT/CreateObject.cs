using MBT;
using UnityEngine;

namespace BT
{
    [MBTNode(name = "Common/CreateObject")]
    public class CreateObject : Leaf
    {
        [SerializeField] private GameObject newGameObject;
        [SerializeField] private Vector3 position;

        public override NodeResult Execute()
        {
            Instantiate(newGameObject, transform.position + position, Quaternion.identity);
            return NodeResult.success;
        }
    }
}
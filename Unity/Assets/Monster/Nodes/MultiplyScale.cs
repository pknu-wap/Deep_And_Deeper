using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/MultiplyScale")]
    public class MultiplyScale : Leaf
    {
        [SerializeField] private float scale = 2;
        private Transform _transform;

        private void Start()
        {
            if (TryGetComponent<Monster>(out var monster))
            {
                _transform = monster.transform;
            }
            else if (transform.parent.TryGetComponent<Monster>(out var parentMonster))
            {
                _transform = parentMonster.transform;
            }
            else
            {
                Debug.LogError("Monster 컴포넌트를 찾을 수 없습니다.");
            }
        }

        public override NodeResult Execute()
        {
            _transform.localScale *= scale;
            return NodeResult.success;
        }
    }
}
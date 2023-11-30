using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/KnightRoll")]
    public class KnightRoll : Leaf
    {
        [SerializeField] private float speed = 8;
        private Monster _monster;

        private void Start()
        {
            if (TryGetComponent<Monster>(out var monster))
            {
                _monster = monster;
            }
            else if (transform.parent.TryGetComponent<Monster>(out var parentMonster))
            {
                _monster = parentMonster;
            }
            else
            {
                Debug.LogError("Monster 컴포넌트를 찾을 수 없습니다.");
            }
        }

        public override NodeResult Execute()
        {
            _monster.SetVelocityX(speed);
            return NodeResult.success;
        }
    }
}
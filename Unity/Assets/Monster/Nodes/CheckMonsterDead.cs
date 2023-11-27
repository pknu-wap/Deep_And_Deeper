using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckMonsterDead")]
    public class CheckMonsterDead : Leaf
    {
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
            return _monster.isDead ? NodeResult.success : NodeResult.failure;
        }
    }
}
using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckPhase2")]
    public class CheckPhase2 : Leaf
    {
        private bool _phase2;
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
            if (_phase2) return NodeResult.failure;
            if (!_monster.phase2) return NodeResult.failure;

            _phase2 = true;
            return NodeResult.success;
        }
    }
}
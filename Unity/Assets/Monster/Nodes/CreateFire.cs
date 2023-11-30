using MBT;
using Monster.Knight;
using UnityEngine;

namespace Monster.Nodes
{
    [MBTNode(name = "Monster/CreateFire")]
    public class CreateFire : Leaf
    {
        [SerializeField] private GameObject newGameObject;
        [SerializeField] private Vector3 position;
        [SerializeField] private int fireDir;

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
            var tempPosition = position;
            tempPosition.x *= _transform.localScale.x > 0 ? 1 : -1;
            var fire = Instantiate(newGameObject, transform.position + tempPosition, Quaternion.identity);

            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            fire.GetComponent<FireCreator>().dir = fireDir;

            return NodeResult.success;
        }
    }
}
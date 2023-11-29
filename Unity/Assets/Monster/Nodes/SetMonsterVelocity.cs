using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/SetMonsterVelocity")]
    public class SetMonsterVelocity : Leaf
    {
        [SerializeField] private bool x, y;
        [SerializeField] private float velocity;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            Debug.Assert(x ^ y);
        }

        private void Start()
        {
            if (TryGetComponent<Monster>(out var monster))
            {
                _rigidbody2D = monster.GetComponent<Rigidbody2D>();
            }
            else if (transform.parent.TryGetComponent<Monster>(out var parentMonster))
            {
                _rigidbody2D = parentMonster.GetComponent<Rigidbody2D>();
            }
            else
            {
                Debug.LogError("Monster 컴포넌트를 찾을 수 없습니다.");
            }
        }

        public override NodeResult Execute()
        {
            if (x) _rigidbody2D.velocity = new Vector2(velocity, _rigidbody2D.velocity.y);
            if (y) _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocity);

            return NodeResult.success;
        }
    }
}
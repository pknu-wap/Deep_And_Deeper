using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("Services/Update Position Service")]
    public class UpdatePositionService : Service
    {
        public TransformReference sourceTransform;
        public Vector3Reference position = new Vector3Reference(VarRefMode.DisableConstant);

        protected override void Task()
        {
            var t = sourceTransform.Value;
            if (t == null)
            {
                return;
            }
            position.Value = t.position;
        }
    }
}

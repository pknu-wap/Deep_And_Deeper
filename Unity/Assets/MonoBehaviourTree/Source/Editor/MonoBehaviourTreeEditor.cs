using UnityEngine;
using UnityEditor;
using MBT;

namespace MBTEditor
{
    [CustomEditor(typeof(MonoBehaviourTree))]
    public class MonoBehaviourTreeEditor : Editor
    {
        private GUIStyle boxStyle;
        private GUIStyle foldStyle;
        private Editor nodeEditor;

        private void InitStyle()
        {
            if (foldStyle != null) return;
            boxStyle = new GUIStyle(EditorStyles.helpBox);
            foldStyle = new GUIStyle(EditorStyles.foldoutHeader);
            foldStyle.onNormal = foldStyle.onFocused;
        }

        private void OnEnable()
        {
            // Set hide flags in case object was duplicated or turned into prefab
            if (target == null)
            {
                return;
            }
            var mbt = (MonoBehaviourTree) target;
            // Sample one component and check if its hidden. Hide all nodes if sample is visible.
            if (!mbt.TryGetComponent<Node>(out var n) || n.hideFlags == HideFlags.HideInInspector) return;
            var nodes = mbt.GetComponents<Node>();
            foreach (var t in nodes)
            {
                t.hideFlags = HideFlags.HideInInspector;
            }
        }

        private void OnDisable()
        {
            // Destroy editor if there is any
            if (nodeEditor != null)
            {
                DestroyImmediate(nodeEditor);
            }
        }

        public override void OnInspectorGUI()
        {
            InitStyle();

            DrawDefaultInspector();
            GUILayout.Space(5);

            if (GUILayout.Button("Open editor")) {
                BehaviourTreeWindow.OpenEditor();
            }

            EditorGUILayout.Space();
            
            var mbt = ((MonoBehaviourTree) target);
            var renderNodeInspector = mbt.selectedEditorNode != null;

            EditorGUILayout.Foldout(renderNodeInspector, "Node inspector", foldStyle);
                EditorGUILayout.Space(1);
                if (renderNodeInspector)
                {
                    EditorGUILayout.BeginHorizontal(boxStyle);
                        GUILayout.Space(3);
                        EditorGUILayout.BeginVertical();
                            GUILayout.Space(5);
                            Editor.CreateCachedEditor(mbt.selectedEditorNode, null, ref nodeEditor);
                            nodeEditor.OnInspectorGUI();
                            GUILayout.Space(5);
                        EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }
            EditorGUILayout.Space();
        }
    }
}

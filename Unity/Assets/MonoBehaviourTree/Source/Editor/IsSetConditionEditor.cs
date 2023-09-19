using UnityEngine;
using UnityEditor;
using MBT;

namespace MBTEditor
{
    [CustomEditor(typeof(IsSetCondition))]
    public class IsSetConditionEditor : Editor
    {
        private SerializedProperty titleProp;
        private SerializedProperty abortProp;
        private SerializedProperty boolReferenceProp;
        private SerializedProperty objectReferenceProp;
        private SerializedProperty transformReferenceProp;
        private SerializedProperty typeProp;
        private SerializedProperty invertProp;

        private void OnEnable()
        {
            titleProp = serializedObject.FindProperty("title");
            boolReferenceProp = serializedObject.FindProperty("boolReference");
            objectReferenceProp = serializedObject.FindProperty("objectReference");
            transformReferenceProp = serializedObject.FindProperty("transformReference");
            abortProp = serializedObject.FindProperty("abort");
            typeProp = serializedObject.FindProperty("type");
            invertProp = serializedObject.FindProperty("invert");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(titleProp);
            EditorGUILayout.PropertyField(abortProp);
            EditorGUILayout.PropertyField(invertProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(typeProp);
            switch (typeProp.enumValueIndex)
            {
                case (int)IsSetCondition.Type.Boolean:
                    EditorGUILayout.PropertyField(boolReferenceProp, new GUIContent("Variable"));
                    break;
                case (int)IsSetCondition.Type.GameObject:
                    EditorGUILayout.PropertyField(objectReferenceProp, new GUIContent("Variable"));
                    break;
                default:
                    EditorGUILayout.PropertyField(transformReferenceProp, new GUIContent("Variable"));
                    break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}

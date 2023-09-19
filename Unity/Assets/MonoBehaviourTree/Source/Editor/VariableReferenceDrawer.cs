using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using MBT;

namespace MBTEditor
{
    [CustomPropertyDrawer(typeof(BaseVariableReference), true)]
    public class VariableReferenceDrawer : PropertyDrawer
    {
        private readonly GUIStyle constVarGUIStyle = new GUIStyle("MiniButton");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginChangeCheck();

            position.height = 18f;
            var keyProperty = property.FindPropertyRelative("key");
            var blackboardProperty = property.FindPropertyRelative("blackboard");
            var useConstProperty = property.FindPropertyRelative("useConstant");
            
            var inspectedComponent = property.serializedObject.targetObject as MonoBehaviour;
            // search only in the same game object
            if (inspectedComponent != null)
            {
                // Blackboard blackboard = inspectedComponent.GetComponent<Blackboard>();
                var blackboard = GetBlackboardInParent(inspectedComponent);
                if (blackboard != null)
                {
                    // Draw mode toggle if not disabled
                    if (property.FindPropertyRelative("mode").enumValueIndex == 0)
                    {
                        var togglePosition = position;
                        togglePosition.width = 8;
                        togglePosition.height = 16;
                        useConstProperty.boolValue = EditorGUI.Toggle(togglePosition, useConstProperty.boolValue, constVarGUIStyle);
                        position.xMin += 10;
                    }
                    
                    // Draw constant or dropdown
                    if (useConstProperty.boolValue)
                    {
                        // Use constant variable
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("constantValue"), label);
                    }
                    else
                    {
                        var desiredVariableType = fieldInfo.FieldType.BaseType.GetGenericArguments()[0];
                        var variables = blackboard.GetAllVariables();
                        var keys = new List<string> { "None" };
                        keys.AddRange(from bv in variables where bv.GetType() == desiredVariableType select bv.key);
                        // Setup dropdown
                        // INFO: "None" can not be used as key
                        var selected = keys.IndexOf(keyProperty.stringValue);
                        if (selected < 0) {
                            selected = 0;
                            // If key is not empty it means variable was deleted and missing
                            if (!string.IsNullOrEmpty(keyProperty.stringValue))
                            {
                                keys[0] = "Missing";
                            }
                        }
                        var result = EditorGUI.Popup(position, label.text, selected, keys.ToArray());
                        if (result > 0) {
                            keyProperty.stringValue = keys[result];
                            blackboardProperty.objectReferenceValue = blackboard;
                        } else {
                            keyProperty.stringValue = "";
                            blackboardProperty.objectReferenceValue = null;
                        }
                    }
                }
                else
                {
                    EditorGUI.LabelField(position, property.displayName);
                    var indent = EditorGUI.indentLevel;
                    EditorGUI.indentLevel = 1;
                    position.y += EditorGUI.GetPropertyHeight(keyProperty);// + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.PropertyField(position, keyProperty);
                    position.y += EditorGUI.GetPropertyHeight(blackboardProperty);// + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.PropertyField(position, blackboardProperty);
                    EditorGUI.indentLevel = indent;
                }
            }
            

            if (EditorGUI.EndChangeCheck()) {
                property.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
            if (monoBehaviour != null && GetBlackboardInParent(monoBehaviour) == null) {
                return 3 * (EditorGUIUtility.standardVerticalSpacing + 16);
            }
            return 16 + EditorGUIUtility.standardVerticalSpacing;
        }

        /// <summary>
        /// Find Blackboard in parent including inactive game objects
        /// </summary>
        /// <param name="component">Component to search</param>
        /// <returns>Blackboard if found, otherwise null</returns>
        private static Blackboard GetBlackboardInParent(Component component)
        {
            var current = component.transform;
            Blackboard result = null;
            while (current != null && result == null)
            {
                if (current.TryGetComponent<Blackboard>(out var b))
                {
                    result = b;
                }
                current = current.parent;
            }
            return result;
        }
    } 
}

using UnityEngine;
using UnityEditor;

namespace MPack
{
    [CustomPropertyDrawer(typeof(AnimationCurveReference))]
    public class AnimationCurveReferenceDraer : RereferenceDrawer
    {
        SerializedProperty valueProperty;

        protected override void OnEnable(SerializedProperty property)
        {
            base.OnEnable(property);
            valueProperty = property.FindPropertyRelative("Value");
        }

        protected override void DrawValue(Rect rest)
        {
            if (useVariableProperty.boolValue)
            {
                DrawVariable(rest);
            }
            else
            {
                DrawValueProperty(rest, valueProperty);
            }
        }

        void DrawVariable(Rect rest)
        {
            Rect objectRect = rest;
            objectRect.width -= 5;
            objectRect.height = 18;
            EditorGUI.PropertyField(objectRect, variableProperty, GUIContent.none);


            Object reference = variableProperty.objectReferenceValue;
            if (reference)
            {
                SerializedObject serializedObject = new SerializedObject(reference);
                serializedObject.Update();
                rest.height = 18;
                rest.y += 20;
                DrawValueProperty(rest, serializedObject.FindProperty("Value"));
                serializedObject.ApplyModifiedProperties();
            }
        }

        void DrawValueProperty(Rect rest, SerializedProperty value)
        {
            rest.width -= 5;
            EditorGUI.PropertyField(rest, value, GUIContent.none);
        }

        protected override void CreateAsset()
        { }
    }
}
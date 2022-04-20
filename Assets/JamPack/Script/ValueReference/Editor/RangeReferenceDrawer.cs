using UnityEngine;
using UnityEditor;

namespace MPack
{
    [CustomPropertyDrawer(typeof(RangeReference))]
    public class RangeReferenceDrawer : RereferenceDrawer
    {
        SerializedProperty minProperty;
        SerializedProperty maxProperty;

        protected override void OnEnable(SerializedProperty property)
        {
            base.OnEnable(property);
            minProperty = property.FindPropertyRelative("Min");
            maxProperty = property.FindPropertyRelative("Max");
        }

        protected override void DrawValue(Rect rest)
        {
            if (useVariableProperty.boolValue)
            {
                DrawVariable(rest);
            }
            else
            {
                DrawMinMax(rest, minProperty, maxProperty);
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
                DrawMinMax(rest, serializedObject.FindProperty("Min"), serializedObject.FindProperty("Max"));
                serializedObject.ApplyModifiedProperties();
            }
        }

        void DrawMinMax(Rect rest, SerializedProperty min, SerializedProperty max)
        {
            Rect minRect = rest;
            minRect.width = rest.width / 2 - 5;
            minRect.height = 18;
            EditorGUI.PropertyField(minRect, min, GUIContent.none);

            Rect maxRect = rest;
            maxRect.width = rest.width / 2 - 5;
            maxRect.height = 18;
            maxRect.x += minRect.width + 5;
            EditorGUI.PropertyField(maxRect, max, GUIContent.none);
        }

        protected override void CreateAsset()
        { }
    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MPJamPack {
    [CustomEditor(typeof(Selectable))]
    public class SelectableEditor : Editor {
        SerializedProperty targetGraphics, style;
        SerializedProperty leftSelectable, rightSelectable, upSelectable, downSelectable;

        bool editingNavigation = false;

        GUIStyle buttonPressed;

        private void SetupStyle() {
            if (buttonPressed != null) return;

            buttonPressed = new GUIStyle("Button");
            buttonPressed.padding = new RectOffset(0, 0, 5, 5);
            buttonPressed.margin = new RectOffset(0, 0, 0, 0);
        }

        protected virtual void OnEnable() {
            targetGraphics = serializedObject.FindProperty("targetGraphics");
            style = serializedObject.FindProperty("style");

            leftSelectable = serializedObject.FindProperty("left");
            rightSelectable = serializedObject.FindProperty("right");
            upSelectable = serializedObject.FindProperty("up");
            downSelectable = serializedObject.FindProperty("down");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SetupStyle();

            EditorGUILayout.PropertyField(targetGraphics, true);
            EditorGUILayout.PropertyField(style);

            GUILayout.Space(5);

            EditorGUI.BeginChangeCheck();
            Selectable.ShowNavigationGizmos = GUILayout.Toggle(Selectable.ShowNavigationGizmos, new GUIContent("Show Navigation"), buttonPressed); ;
            if (EditorGUI.EndChangeCheck()) {
                EditorWindow.GetWindow<SceneView>().Repaint();
            }

            GUILayout.Space(5);
            editingNavigation = GUILayout.Toggle(editingNavigation, new GUIContent("Edit Navigation"), buttonPressed);
            GUILayout.Space(5);

            if (editingNavigation) {
                EditorGUILayout.PropertyField(leftSelectable);
                EditorGUILayout.PropertyField(rightSelectable);
                EditorGUILayout.PropertyField(upSelectable);
                EditorGUILayout.PropertyField(downSelectable);

                if (GUILayout.Button("Auto Generate Navigation")) {
                    Selectable selectable = (Selectable) target;
                    selectable.GenerateNavigation();
                    EditorWindow.GetWindow<SceneView>().Repaint();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }


    [CustomEditor(typeof(SelectableButton))]
    public class SelectableButtonEditor : SelectableEditor
    {
        [MenuItem("GameObject/MPJamPack/Button", false, 0)]
        static public void OnCreate()
        {
            GameObject obj = new GameObject("Button", typeof(RectTransform));

            if (Selection.activeGameObject)
            {
                obj.GetComponent<RectTransform>().parent = Selection.activeGameObject.transform;
            }
            else
            {
                obj.GetComponent<RectTransform>().parent = FindObjectOfType<Canvas>().transform;
            }
            obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            obj.AddComponent<SelectableButton>();

            Selection.activeGameObject = obj;
        }

        SerializedProperty submitEvent;

        protected override void OnEnable() {
            base.OnEnable();

            submitEvent = serializedObject.FindProperty("submitEvent");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(submitEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }

    
    [CustomEditor(typeof(SelectableSideSet))]
    public class SelectableSideSetEditor : SelectableEditor {
        [MenuItem("GameObject/MPJamPack/Side Set", false)]
        static public void OnCreate()
        {
            GameObject obj = new GameObject("SideSet", typeof(RectTransform));

            if (Selection.activeGameObject)
                obj.GetComponent<RectTransform>().parent = Selection.activeGameObject.transform;
            else
                obj.GetComponent<RectTransform>().parent = FindObjectOfType<Canvas>().transform;

            obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            obj.AddComponent<SelectableSideSet>();

            Selection.activeGameObject = obj;
        }

        SerializedProperty submitEvent, leftEvent, rightEvent;

        protected override void OnEnable()
        {
            base.OnEnable();

            submitEvent = serializedObject.FindProperty("submitEvent");
            leftEvent = serializedObject.FindProperty("leftEvent");
            rightEvent = serializedObject.FindProperty("rightEvent");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(submitEvent);
            EditorGUILayout.PropertyField(leftEvent);
            EditorGUILayout.PropertyField(rightEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
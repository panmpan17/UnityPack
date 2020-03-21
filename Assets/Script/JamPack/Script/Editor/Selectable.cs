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

        // private void GenerateNavigation(Canvas canvas) {
        //     Selectable[] selectables = FindObjectsOfType<Selectable>();

        //     for (int i = 0; i < selectables.Length; i++) {
        //         RectTransform rectT = selectables[i].GetComponent<RectTransform>();
        //         float minX = rectT.position.x - rectT.sizeDelta.x / 2;
        //         float maxX = rectT.position.x + rectT.sizeDelta.x / 2;
        //         float minY = rectT.position.y - rectT.sizeDelta.y / 2;
        //         float maxY = rectT.position.y + rectT.sizeDelta.y / 2;

        //         float bestRightDis;
        //         Selectable bestRight;
        //         for (int j = 0; j < selectables.Length; j++)
        //         {
        //             if (selectables[j] == selectables[i]) continue;

        //             RectTransform rectT2 = selectables[j].GetComponent<RectTransform>();
        //             float minX2 = rectT2.position.x - rectT2.sizeDelta.x / 2;
        //             float maxX2 = rectT2.position.x + rectT2.sizeDelta.x / 2;
        //             float minY2 = rectT2.position.y - rectT2.sizeDelta.y / 2;
        //             float maxY2 = rectT2.position.y + rectT2.sizeDelta.y / 2;

        //             float rightDis = minX2 - maxX;
        //             if (selectables[i].Type == SelectableType.Button && selectables[i].right == null) {
        //                 if ()
        //             }
        //         }
        //     }
        // }
    }


    [CustomEditor(typeof(SelectableButton))]
    public class SelectableButtonEditor : SelectableEditor
    {
        [MenuItem("GameObject/MPJamPack/Selectable Button", false, 0)]
        static public void OnCreate()
        {
            GameObject obj = new GameObject("Selectable Button", typeof(RectTransform));

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

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
        }
    }
}
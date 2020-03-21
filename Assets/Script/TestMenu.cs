using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPJamPack;
using UnityEditor;

public class TestMenu : AbstractPauseMenu
{
    [CustomEditor(typeof(TestMenu))]
    public class _Editor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
            if(GUILayout.Button("Activate")) {
                TestMenu menu = (TestMenu) target;
                menu.Activate();
            }
        }
    }
}

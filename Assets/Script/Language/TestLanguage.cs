using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPack;

public class TestLanguage : MonoBehaviour
{
    [SerializeField]
    LanguageData languagePack, secondLanguagePack;
    private bool main = true;

    private void Start() {
        LanguageMgr.AssignLanguageData(languagePack);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            main = !main;
            LanguageMgr.AssignLanguageData(main? languagePack: secondLanguagePack);
        }
    }
}

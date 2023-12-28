using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPack;

public class TestLanguage : MonoBehaviour
{
    [SerializeField, LanguageID]
    private int languageID;
    [SerializeField, LanguageID]
    private int[] languageIDs;
}

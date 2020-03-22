using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPJamPack;

public class TestAudio : MonoBehaviour
{
    [SerializeField]
    VirtualAudioManager audioManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            audioManager.PlayOneShot(AudioIDEnum.Shoot);
        }

        if (Input.GetMouseButtonDown(1))
        {
            audioManager.PlayOneShotAtPosition(AudioIDEnum.Shoot, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}

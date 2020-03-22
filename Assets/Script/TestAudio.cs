using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPJamPack;

public class TestAudio : MonoBehaviour
{
    [SerializeField]
    VirtualAudioManager audioManager;

    [SerializeField]
    private AudioClip bgm1, bgm2;

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

        if (Input.GetKeyDown(KeyCode.A)) {
            audioManager.PlayBgm(bgm1, true);
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            audioManager.BlendNewBgm(bgm2, fadeOut: 2);
        }
    }
}

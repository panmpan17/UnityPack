using UnityEngine;


namespace MPack
{
    public class BGMTrigger : MonoBehaviour
    {
        [SerializeField]
        private AudioClip bgm;
        [SerializeField]
        private float lastBgmFadeOutDelay = 0f;
        [SerializeField]
        private float lastBgmFadeOutTime = 1f;
        [SerializeField]
        private float bgmFadeInDelayTime = 0.5f;
        [SerializeField]
        private float bgmFadeInTime = 1f;

        [SerializeField]
        private bool triggOnStart;

        void Start()
        {
            if (triggOnStart)
                Trigger();
        }

        public void Trigger()
        {
            if (bgm == null)
                return;
            if (VirtualAudioManager.ins == null)
                return;

            VirtualAudioManager.ins.BlendNewBgm(bgm, lastBgmFadeOutTime, lastBgmFadeOutDelay, bgmFadeInTime, bgmFadeInDelayTime);
        }
    }
}
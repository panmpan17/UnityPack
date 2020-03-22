using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPJamPack {
    public class VirtualAudioManager : MonoBehaviour
    {
        private Dictionary<int, AudioClip> clipDicts;

        public AudioSource oneShotAudioSrc;
        private PrefabPool<AudioOneShotPlayer> oneShotPlayerPool;

        [SerializeField]
        private AudioPreset defaultPreset;

        private void Awake() {
            clipDicts = new Dictionary<int, AudioClip>();

            oneShotPlayerPool = new PrefabPool<AudioOneShotPlayer>(delegate {
                GameObject obj = new GameObject("AudioOneShotPlayer");
                return obj.AddComponent<AudioOneShotPlayer>();
            }, true, "ParticleCollection");

            if (defaultPreset != null) LoadAudioPreset(defaultPreset);
        }

        private void Start() {
            if (oneShotAudioSrc == null) {
                oneShotAudioSrc = gameObject.AddComponent<AudioSource>();
            }
        }

    #region Audio Load/ Unload
        public void LoadAudioPreset(AudioPreset preset, bool overrideExist=false) {
            for (int i = 0; i < preset.Audios.Length; i++)
            {
                int ID = (int) preset.Audios[i].Type;

                if (clipDicts.ContainsKey(ID))
                {
                    if (!overrideExist) {
                    #if UNITY_EDITOR
                        Debug.LogWarningFormat("Audio '{0}' already exist", ID);
                    #endif
                        continue;
                    }
                    else clipDicts[ID] = preset.Audios[i].Clip;
                }
                else clipDicts.Add(ID, preset.Audios[i].Clip);
            }
        }

        public void LoadAudio(int ID, AudioClip clip, bool overrideExist=false) {
            if (clipDicts.ContainsKey(ID))
            {
                if (!overrideExist)
                {
                #if UNITY_EDITOR
                    Debug.LogWarningFormat("Audio '{0}' already exist", ID);
                #endif
                }
                else clipDicts[ID] = clip;
            }
            else clipDicts.Add(ID, clip);
        }

        public void UnloadAudioPreset(AudioPreset preset) {
            for (int i = 0; i < preset.Audios.Length; i++)
            {
                int ID = (int)preset.Audios[i].Type;

                if (clipDicts.ContainsKey(ID)) clipDicts.Remove(ID);
            }
        }

        public void UnloadAudio(int ID) {
            if (clipDicts.ContainsKey(ID)) clipDicts.Remove(ID);
        }
    #endregion

    #region Audio One Shot
        public void PlayOneShot(AudioIDEnum enumID, float volumeMultiplier = 1) {
            int ID = (int) enumID;
            if (clipDicts.ContainsKey(ID)) {
                oneShotAudioSrc.PlayOneShot(clipDicts[ID], volumeMultiplier);
            }
            else {
            #if UNITY_EDITOR
                Debug.LogWarningFormat("Audio '{0}' doesn't exist", ID);
            #endif
            }
        }

        public void PlayOneShot(int ID, float volumeMultiplier=1) {
            if (clipDicts.ContainsKey(ID)) {
                oneShotAudioSrc.PlayOneShot(clipDicts[ID], volumeMultiplier);
            }
            else {
            #if UNITY_EDITOR
                Debug.LogWarningFormat("Audio '{0}' doesn't exist", ID);
            #endif
            }
        }

        public void PlayOneShot(AudioClip clip, float volumeMultiplier = 1) {
            oneShotAudioSrc.PlayOneShot(clip, volumeMultiplier);
        }
    #endregion

    #region Gameobject's Audio One Shot
        public void PlayOneShotAtPosition(AudioIDEnum enumID, Vector3 position, float volumeMultiplier=1) {
            int ID = (int) enumID;
            if (!clipDicts.ContainsKey(ID)) {
            #if UNITY_EDITOR
                Debug.LogWarningFormat("Audio '{0}' doesn't exist", ID);
            #endif
                return;
            }

            AudioOneShotPlayer player = oneShotPlayerPool.Get();
            player.transform.position = position;
            player.Play(clipDicts[ID], (_player) => oneShotPlayerPool.Put(_player), volumeMultiplier);
        }
    #endregion
    }
}
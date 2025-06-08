using UnityEngine;


namespace MPack
{
    [CreateAssetMenu(menuName="MPack/Audio One Shot Pool Reference")]
    public class AudioOneShotPlayerPoolReference : ScriptableObject
    {
        public AudioOneShotPlayer Prefab;
        public int IntialCount;
        public bool CreateCollection;
        public string CollectName;

        private PrefabPool<AudioOneShotPlayer> _pool;

        public void Setup(Transform parent)
        {
            _pool = new PrefabPool<AudioOneShotPlayer>(Prefab, parent: parent);
            _pool.Initialize(IntialCount);
        }

        public void PlayAtPosition(Vector3 position, AudioClip clip, float volume=1, float pitch=1, System.Action<AudioOneShotPlayer> endCallback=null)
        {
            AudioOneShotPlayer player = _pool.Get();
            player.transform.position = position;
            player.Play(clip, volume: volume, pitch: pitch, playEndCall: endCallback);
        }
    }
}
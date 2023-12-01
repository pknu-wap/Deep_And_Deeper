using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public enum Sound
    {
        PlayerHurt,
        PlayerDead,
        MonsterHurt,
        MonsterDead,
    }

    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;

        public static SoundManager Instance
        {
            get
            {
                if (_instance) return _instance;

                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                _instance = new GameObject().AddComponent<SoundManager>();

                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }

        private readonly Dictionary<Sound, AudioClip> _audioClips = new();
        private readonly Dictionary<Sound, AudioSource> _audioSources = new();

        private void Awake()
        {
            var data = Resources.Load<SoundManagerData>("SoundManagerDataContainer");

            _audioClips[Sound.PlayerHurt] = data.PlayerHurt;
            _audioClips[Sound.PlayerDead] = data.PlayerDead;
            _audioClips[Sound.MonsterHurt] = data.MonsterHurt;
            _audioClips[Sound.MonsterDead] = data.MonsterDead;

            var prefab = data.audioSourcePrefab;

            foreach (var (sound, audioClip) in _audioClips)
            {
                var source = Instantiate(prefab).GetComponent<AudioSource>();
                source.clip = audioClip;
                _audioSources[sound] = source;
            }
        }

        public void PlaySfx(Sound sound)
        {
            _audioSources[sound].Play();
        }
    }
}
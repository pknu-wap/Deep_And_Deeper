using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    public enum Sound
    {
        PlayerHurt,
        PlayerDead,
        MonsterHurt,
        MonsterDead,
        Stage1,
        Stage2,
        Stage3,
        Boss1,
        Boss2,
        Boss3,
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
                _instance = new GameObject("Sound Manager").AddComponent<SoundManager>();

                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }

        private GameObject _audioSourcePrefab;

        private readonly Dictionary<Sound, AudioContainer> _audioContainers = new();
        private readonly Dictionary<Sound, AudioSource> _audioSources = new();

        private void Init()
        {
            var data = Resources.Load<SoundManagerData>("SoundManagerDataContainer");

            _audioContainers[Sound.PlayerHurt] = data.PlayerHurt;
            _audioContainers[Sound.PlayerDead] = data.PlayerDead;
            _audioContainers[Sound.MonsterHurt] = data.MonsterHurt;
            _audioContainers[Sound.MonsterDead] = data.MonsterDead;
            _audioContainers[Sound.Stage1] = data.Stage1;
            _audioContainers[Sound.Stage2] = data.Stage2;
            _audioContainers[Sound.Stage3] = data.Stage3;
            _audioContainers[Sound.Boss1] = data.Boss1;
            _audioContainers[Sound.Boss2] = data.Boss2;
            _audioContainers[Sound.Boss3] = data.Boss3;

            _audioSourcePrefab = data.audioSourcePrefab;

            OnSceneLoaded();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Awake()
        {
            Init();
        }

        private void OnSceneLoaded()
        {
            if (GameObject.FindGameObjectWithTag("SoundSource") != null) return;

            var parent = new GameObject("SoundSources").transform;

            foreach (var (sound, audioContainer) in _audioContainers)
            {
                var source = Instantiate(_audioSourcePrefab, parent).GetComponent<AudioSource>();
                source.clip = audioContainer.audioClip;
                source.volume = audioContainer.volume;
                source.loop = audioContainer.isLoop;
                _audioSources[sound] = source;
            }
        }

        private void OnSceneLoaded(Scene a, LoadSceneMode b)
        {
            OnSceneLoaded();
        }

        public void PlaySfx(Sound sound)
        {
            _audioSources[sound].Play();
        }

        public void StopAll()
        {
            if (_audioSources.Count == 0) return;

            foreach (var audioSource in _audioSources.Values)
            {
                audioSource.Stop();
            }
        }
    }
}
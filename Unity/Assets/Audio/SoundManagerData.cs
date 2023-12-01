using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class AudioContainer
    {
        public AudioClip audioClip;
        public bool isLoop;
        public float volume = 1;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SoundManagerData : MonoBehaviour
    {
        public AudioContainer PlayerHurt;
        public AudioContainer PlayerDead;
        public AudioContainer MonsterHurt;
        public AudioContainer MonsterDead;
        public AudioContainer Stage1;
        public AudioContainer Stage2;
        public AudioContainer Stage3;
        public AudioContainer Boss1;
        public AudioContainer Boss2;
        public AudioContainer Boss3;
        public GameObject audioSourcePrefab;
    }
}
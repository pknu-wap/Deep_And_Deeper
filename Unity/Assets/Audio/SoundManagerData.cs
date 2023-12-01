using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Audio
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SoundManagerData : MonoBehaviour
    {
        public AudioClip PlayerHurt;
        public AudioClip PlayerDead;
        public AudioClip MonsterHurt;
        public AudioClip MonsterDead;
        public GameObject audioSourcePrefab;
    }
}
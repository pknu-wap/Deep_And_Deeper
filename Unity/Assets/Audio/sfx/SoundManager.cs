using System.Collections;
using System.Collections.Generic;using Microsoft.Win32.SafeHandles;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; //효과음 이름
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] Sound[] sfx = null;
    
    [SerializeField] AudioSource[] sfxPlayer = null;

    private void Start()
    {
        instance = this;
    }

    public void PlaySfx(string sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    // SFXPlayer에서 재생 중이지 않은 Audio Source를 발견했다면 
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중");
                return;
            }
        }
        Debug.Log(sfxName + " 이름의 효과음이 없음");
        return;
    }
}

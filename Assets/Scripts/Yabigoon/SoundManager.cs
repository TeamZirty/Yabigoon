using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    [Header("사운드 클립")]
    public AudioClip shootSFX; // 총 발사 효과음
    public AudioClip enemyDeathSFX; // 적 사망 효과음
    public AudioClip levelBGM; // 레벨 배경음악
    public AudioClip menuBGM; // 메뉴 배경음악

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxAudioSource != null && clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmAudioSource != null && clip != null)
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
    }
}
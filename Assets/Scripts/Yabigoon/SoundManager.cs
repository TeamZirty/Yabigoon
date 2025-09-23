using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    [Header("���� Ŭ��")]
    public AudioClip shootSFX; // �� �߻� ȿ����
    public AudioClip enemyDeathSFX; // �� ��� ȿ����
    public AudioClip levelBGM; // ���� �������
    public AudioClip menuBGM; // �޴� �������

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

    // --- BGM Switching Logic ---

    [Header("Background Music")]
    public AudioClip[] bgmTracks; // Assign your BGM clips here in the Inspector
    private int currentTrackIndex = 0;

    void Start()
    {
        // Play the first BGM track on start, if it exists
        if (bgmTracks != null && bgmTracks.Length > 0)
        {
            // Make sure the BGM source is set to loop
            bgmAudioSource.loop = true;
            PlayBGM(bgmTracks[currentTrackIndex]);
        }
    }

    void Update()
    {
        // Check for input to change the BGM
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeToNextBGM();
        }
    }

    // Switches to the next BGM in the tracks list
    public void ChangeToNextBGM()
    {
        if (bgmTracks == null || bgmTracks.Length <= 1)
        {
            return; // No tracks to switch to
        }

        // Move to the next track
        currentTrackIndex++;

        // If we've gone past the end of the list, loop back to the start
        if (currentTrackIndex >= bgmTracks.Length)
        {
            currentTrackIndex = 0;
        }

        // Play the new track
        PlayBGM(bgmTracks[currentTrackIndex]);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Audio Clip In Menu")]
    [SerializeField] private AudioClip soundClip_;

    [Header("Audio Clip In Game")]
    [SerializeField] private AudioClip musicsClips_;

    [Header("Audio Source")]
    [SerializeField] private AudioSource soundSource_;
    [SerializeField] private AudioSource musicSource_;

    public static bool soundON_ = true;
    public static bool musicON_ = true;

    private void Awake()
    {
        StartAudioGame();
    }

    private void Update()
    {
        PlayMusic();
    }

    #region System Audio

    private void StartAudioGame()
    {
        soundON_ = true;
        musicON_ = true;
    }

    private void PlayMusic()
    {
        if (musicON_)
        {
            musicSource_.clip = musicsClips_;
            if (!musicSource_.isPlaying)
                musicSource_.Play();
        }
        else
        {
            musicSource_.Stop();
        }
    }

    public void OnClickButtonPlaySound()
    {
        if (soundON_)
        {
            soundSource_.clip = soundClip_;
            soundSource_.Play();
        }
    }

    #endregion
}

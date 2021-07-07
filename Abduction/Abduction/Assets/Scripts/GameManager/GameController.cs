using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Audio Clip In Menu")]
    [SerializeField] private AudioClip soundClip_;

    [Header("Audio Clip In Game")]
    [SerializeField] private AudioClip musicsClips_;

    [Header("Animator scenery")]
    [SerializeField] private Animator animatorScenery_;
    private string parameterAnimatorScenery_ = "down";
    private bool sceneryDown_;
    private float timeSceneryDown_;

    [Header("Audio Source")]
    [SerializeField] private AudioSource soundSource_;
    [SerializeField] private AudioSource musicSource_;

    public static bool soundON_ = true;
    public static bool musicON_ = true;

    public static bool startGame_ = false;
    public static bool pauseGame_ = false;
    public static bool cutSceneStartGame_ = false;
    public static bool cutSceneStartGameRunnable_ = false;

    private float timeCut_;
    private float timeCutMax_;

    private void Awake()
    {
        StartAudioGame();
    }

    private void Update()
    {
        PlayMusic();
        StartGameCutScene();
    }

    #region System CutScene Controll

    private void StartGameCutScene()
    {
        if (startGame_)
        {
            if (startGame_ && !cutSceneStartGameRunnable_)
            {
                cutSceneStartGame_ = true;
            }

            if (cutSceneStartGameRunnable_)
            {
                sceneryDown_ = true;
            }

            if (sceneryDown_)
            {
                timeSceneryDown_ += Time.deltaTime;
            }

            if (timeSceneryDown_ >= 1.5f)
            {
                timeSceneryDown_ = 0;
                sceneryDown_ = false;
                animatorScenery_.SetBool(parameterAnimatorScenery_, true);
            }
        }
        else
        {
            cutSceneStartGame_ = false;
            sceneryDown_ = false;
            timeSceneryDown_ = 0;
            animatorScenery_.SetBool(parameterAnimatorScenery_, false);
        }
    }

    #endregion

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

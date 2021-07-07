using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipBackground : MonoBehaviour
{
    private float time_ = 0;
    private float maxTime_ = 45;

    private bool playingAniamtor_;

    [SerializeField] private Animator animator_;
    [SerializeField] private GameObject spaceship_;
    [SerializeField] private Transform pointToStartPos_;

    private string parameterAnimator_ = "play";

    private void Awake()
    {
        SetComponent();
        playingAniamtor_ = true;
    }

    private void Update()
    {
        ControllSpaceship();
        CheckPlayGame();
    }

    private void SetComponent()
    {
        if (animator_ == null)
            animator_ = GetComponent<Animator>();
    }

    private void CheckPlayGame()
    {
        if (GameController.startGame_)
        {
            spaceship_.SetActive(false);
            spaceship_.transform.position = pointToStartPos_.position;
        }
        else
        {
            spaceship_.SetActive(true);
        }
    }

    private void ControllSpaceship()
    {
        if (playingAniamtor_)
        {
            animator_.SetBool(parameterAnimator_, true);
            playingAniamtor_ = false;
        }
        else
        {
            animator_.SetBool(parameterAnimator_, false);
        }

        if (!playingAniamtor_)
        {
            time_ += Time.deltaTime;
        }

        if (time_ >= maxTime_)
        {
            time_ = 0;
            playingAniamtor_ = true;
        }
    }
}

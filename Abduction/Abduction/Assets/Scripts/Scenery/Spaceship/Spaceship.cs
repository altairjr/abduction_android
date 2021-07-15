using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private Animator animator_;

    private string parametersAnimator_01_ = "down";
    private string parametersAnimator_02_ = "up";

    private float time_;

    private void Awake()
    {
        SetComponent();
    }

    private void Update()
    {
        RunCutScene();
    }

    private void SetComponent()
    {
        if (animator_ == null)
            animator_ = GetComponent<Animator>();
    }

    private void RunCutScene()
    {
        if (GameController.startGame_)
        {
            if (GameController.cutSceneStartGame_ && !GameController.cutSceneStartGameRunnable_)
            {
                time_ += Time.deltaTime;
                animator_.SetBool(parametersAnimator_01_, true);
                animator_.SetBool(parametersAnimator_02_, false);
            }

            if (time_ >= 3)
            {
                time_ = 0;
                animator_.SetBool(parametersAnimator_01_, false);
                animator_.SetBool(parametersAnimator_02_, true);
                GameController.cutSceneStartGame_ = false;
                GameController.cutSceneStartGameRunnable_ = true;
            }
        }
        else
        {
            time_ = 0;
            animator_.SetBool(parametersAnimator_01_, false);
            animator_.SetBool(parametersAnimator_02_, false);
            GameController.cutSceneStartGame_ = false;
            GameController.cutSceneStartGameRunnable_ = false;
        }
    }
}

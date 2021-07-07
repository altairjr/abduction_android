using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Position of cow")]
    [SerializeField] private Transform positionAbductionRight_;
    [SerializeField] private Transform positionAbductionLeft_;
    [SerializeField] private Transform posStart_;


    private float speedMove_ = 2f;
    private int inputPos_ = 0;

    private Animator animator_;
    private SpriteRenderer cowSprinte_;

    private string parameterAbductionAnimator = "abduction";
    private float timeCutScene_;

    private void Awake()
    {
        SetComponent();
        inputPos_ = 0;
    }

    private void Update()
    {
        StartAbduction();
        InputPlayer();
        MovePlayer();
    }

    private void SetComponent()
    {
        if (animator_ == null)
            animator_ = GetComponent<Animator>();

        if (cowSprinte_ == null)
            cowSprinte_ = GetComponent<SpriteRenderer>();
    }

    private void StartAbduction()
    {
        if (GameController.startGame_)
        {
            if (GameController.cutSceneStartGame_)
            {
                timeCutScene_ += Time.deltaTime;
            }

            if (timeCutScene_ >= 2.8f)
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, positionAbductionRight_.position, 0.14f * Time.deltaTime);
                animator_.SetBool(parameterAbductionAnimator, true);
            }
        }
        else
        {
            timeCutScene_ = 0;
            animator_.SetBool(parameterAbductionAnimator, false);
            transform.position = new Vector2(gameObject.transform.position.x, posStart_.position.y);
        }
    }
    
    private void InputPlayer()
    {
        if (GameController.startGame_ && !GameController.pauseGame_)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (inputPos_ == 0)
                {
                    inputPos_ = 1;
                }
                else if (inputPos_ == 1)
                {
                    inputPos_ = 0;
                }
            }
        }
    }

    private void MovePlayer()
    {
        if(GameController.startGame_ && !GameController.pauseGame_ && !GameController.cutSceneStartGame_)
        {
            if (inputPos_ == 0)
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, positionAbductionRight_.position, speedMove_ * Time.deltaTime);
                cowSprinte_.flipX = false;
            }

            if (inputPos_ == 1)
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, positionAbductionLeft_.position, speedMove_ * Time.deltaTime);
                cowSprinte_.flipX = true;
            }
        }
    }
}

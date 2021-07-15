using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Sistema para identificar a posição do player na tela
    [Header("Position of cow")]
    [SerializeField] private Transform positionAbductionRight_;
    [SerializeField] private Transform positionAbductionLeft_;
    [SerializeField] private Transform posStart_;
    private float speedMove_ = 4f;
    private int inputPos_ = 0;
    private bool posRight_;
    private bool posLeft_;

    [Header("Position")]
    public float posY_02_;
    public float posY_01_;

    private Vector2 positionAbductionAll_y_;

    [Header("Camera Shake")]
    [SerializeField] private CameraShake cameraShake_;

    [Header("System coins for UI")]
    [SerializeField] private Text textCoinsInGame_;

    [Header("System point for UI")]
    [SerializeField] private Text textPointsInGame_;

    // Sistema de moedas
    private int coins_ = 0;

    // Sistema de pontuação do player
    private int points;
    private float countPoints;
    private float multiplierPoints = 10;

    // Sistema para identificar se o player está vivo
    private int life_ = 2;
    private bool death_;

    // Componentes do player
    private Animator animator_;
    private SpriteRenderer cowSprinte_;
    private MenuController menuController_;

    private string parameterAbductionAnimator = "abduction";
    private float timeCutScene_;

    private void Awake()
    {
        SetComponent();
        inputPos_ = 0;
    }

    private void Start()
    {
        LoadStatus();
    }

    private void Update()
    {
        StartAbduction();
        InputPlayer();
        MovePlayer();
        Reset();

        PlayerPoints();
        CheckLifePlayer();
        SetCoinsAndPointsUI();
    }

    #region InputPlayer

    private void InputPlayer()
    {
        if (GameController.startGame_ && !GameController.pauseGame_ && !GameController.clickButtonUI_ && GameController.cutSceneStartGameRunnable_)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (inputPos_ == 0 && !posLeft_)
                {
                    inputPos_ = 1;
                }
                else if (inputPos_ == 1 && !posRight_)
                {
                    inputPos_ = 0;
                }
            }
        }
    }

    #endregion

    #region Player Controller

    private void MovePlayer()
    {
        if(GameController.startGame_ && !GameController.pauseGame_ && !GameController.cutSceneStartGame_ && GameController.cutSceneStartGameRunnable_)
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

    private void CheckLifePlayer()
    {

        if (life_ == 2)
        {
            positionAbductionAll_y_ = new Vector2(0, posY_02_);
            positionAbductionRight_.position = new Vector2(positionAbductionRight_.position.x, positionAbductionAll_y_.y);
            positionAbductionLeft_.position = new Vector2(positionAbductionLeft_.position.x, positionAbductionAll_y_.y);
        }

        if(life_ == 1)
        {
            positionAbductionAll_y_ = new Vector2(0, posY_01_);
            positionAbductionRight_.position = new Vector2(positionAbductionRight_.position.x, positionAbductionAll_y_.y);
            positionAbductionLeft_.position = new Vector2(positionAbductionLeft_.position.x, positionAbductionAll_y_.y);
        }

        if(life_ == 0)
        {
            death_ = true;
        }
    }

    #endregion

    #region GameController

    private void SetComponent()
    {
        if (animator_ == null)
            animator_ = GetComponent<Animator>();

        if (cowSprinte_ == null)
            cowSprinte_ = GetComponent<SpriteRenderer>();

        if (menuController_ == null)
            menuController_ = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuController>();
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
            transform.position = new Vector2(posStart_.position.x, posStart_.position.y);
        }
    }

    private void Reset()
    {
        if (!GameController.startGame_)
        {
            life_ = 2;
            points = 0;
            death_ = false;
            inputPos_ = 0;
            posRight_ = true;
            posLeft_ = false;
            timeCutScene_ = 0;
            cowSprinte_.flipX = false;
            GameController.clickButtonUI_ = false;
            animator_.SetBool(parameterAbductionAnimator, false);
            transform.position = new Vector2(posStart_.position.x, posStart_.position.y);
        }

        if (death_)
        {
            points = 0;
            GameController.startGame_ = false;
            menuController_.Reset();
            SaveStatus();
        }
    }

    void PlayerPoints()
    {
        if(GameController.startGame_ && GameController.cutSceneStartGameRunnable_)
        {
            countPoints += (Time.deltaTime * multiplierPoints);
            if (countPoints >= 1f)
            {
                points++;
                countPoints = 0;
            }
        }
    }

    private void SetCoinsAndPointsUI()
    {
        textCoinsInGame_.text = coins_.ToString();
        textPointsInGame_.text = points.ToString();
    }

    private void SaveStatus()
    {
        PlayerPrefs.SetInt("Coins", coins_);
        PlayerPrefs.Save();
    }

    private void LoadStatus()
    {
        coins_ = PlayerPrefs.GetInt("Coins");
    }

    #endregion

    #region Trigger Controller

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("posAbductionRight"))
        {
            posRight_ = true;
            posLeft_ = false;
        }

        if (collision.CompareTag("posAbductionLeft"))
        {
            posLeft_ = true;
            posRight_ = false;
        }

        if (collision.CompareTag("Obstacles"))
        {
            life_--;
            StartCoroutine(cameraShake_.Shake(.15f, 250f));
        }

        if (collision.CompareTag("Coins"))
        {
            coins_++;
        }
    }

    #endregion
}

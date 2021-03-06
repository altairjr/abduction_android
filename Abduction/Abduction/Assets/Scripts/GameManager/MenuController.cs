using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] public GameObject startMenu_;
    [SerializeField] public GameObject settingtMenu_;
    [SerializeField] public GameObject inGameMenu_;
    private GameObject menuActive_;

    [Header("Language EN")]
    [SerializeField] private GameObject[] language_en_;
    [SerializeField] private GameObject buttonActiveEN_;
    private bool selectEN_;

    [Header("Language PT")]
    [SerializeField] private GameObject[] language_pt_;
    [SerializeField] private GameObject buttonActivePT_;
    private bool selectPT_;

    [Header("Sound On and OFF and Music")]
    [SerializeField] private GameObject buttonSoundON_;
    [SerializeField] private GameObject buttonSoundOFF_;
    [Space]
    [SerializeField] private GameObject buttonMusicON_;
    [SerializeField] private GameObject buttonMusicOFF_;

    public static MenuController Instancia { get; set; }

    private void Awake()
    {
        ChangeMenu(startMenu_);;
        CLanguagehangeButton(buttonActiveEN_);
    }

    private void Update()
    {
        SetLanguage();
        SetButttonAudio();
    }

    #region Check click UI in game

    public void EnterClickButtons()
    {
        GameController.clickButtonUI_ = true;
        Debug.Log(GameController.clickButtonUI_);
    }

    public void ExitClickButtons()
    {
        GameController.clickButtonUI_ = false;
        Debug.Log(GameController.clickButtonUI_);
    }

    #endregion

    #region Menu Controller

    public void ChangeMenu(GameObject menu)
    {
        startMenu_.SetActive(false);
        settingtMenu_.SetActive(false);
        inGameMenu_.SetActive(false);

        menu.SetActive(true);
    }

    public void Reset()
    {
        ChangeMenu(startMenu_);
    }

    public void OnClickYesQuitGame()
    {
        Application.Quit();
    }

    public void OnClickStartGame()
    {
        GameController.startGame_ = true;
    }

    public void OnClickSettingsInGame()
    {
        if (GameController.startGame_)
            GameController.pauseGame_ = true;
    }

    public void OnClickReturn()
    {
        if (GameController.startGame_)
        {
            GameController.pauseGame_ = false;
            GameController.clickButtonUI_ = false;
            ChangeMenu(inGameMenu_);
        }
        else
        {
            ChangeMenu(startMenu_);
        }
    }

    public void OnClickReturnMenu()
    {
        ChangeMenu(startMenu_);
        GameController.startGame_ = false;
    }

    #endregion

    #region Menu Controller Audio

    private void SetButttonAudio()
    {
        if (GameController.soundON_)
        {
            ChangeButtonSound(buttonSoundON_);
        }
        else
        {
            ChangeButtonSound(buttonSoundOFF_);
        }

        if (GameController.musicON_)
        {
            ChangeButtonMusic(buttonMusicON_);
        }
        else
        {
            ChangeButtonMusic(buttonMusicOFF_);
        }
    }

    private void ChangeButtonSound(GameObject sound)
    {
        buttonSoundON_.SetActive(false);
        buttonSoundOFF_.SetActive(false);

        sound.SetActive(true);
    }

    private void ChangeButtonMusic(GameObject music)
    {
        buttonMusicON_.SetActive(false);
        buttonMusicOFF_.SetActive(false);

        music.SetActive(true);
    }

    public void OnClickButtonSound()
    {
        GameController.soundON_ = !GameController.soundON_;
    }

    public void OnClickButtonMusic()
    {
        GameController.musicON_ = !GameController.musicON_;
    }

    #endregion

    #region Set Language

    private void SetLanguage()
    {
        if (selectEN_)
        {
            CLanguagehangeButton(buttonActiveEN_);
            for(int i = 0; i < language_en_.Length; i++)
            {
                language_en_[i].SetActive(true);
            }

            for (int i = 0; i < language_pt_.Length; i++)
            {
                language_pt_[i].SetActive(false);
            }
        }


        if (selectPT_)
        {
            CLanguagehangeButton(buttonActivePT_);
            for (int i = 0; i < language_pt_.Length; i++)
            {
                language_pt_[i].SetActive(true);
            }

            for (int i = 0; i < language_en_.Length; i++)
            {
                language_en_[i].SetActive(false);
            }
        }
    }
    private void CLanguagehangeButton(GameObject button)
    {
        buttonActiveEN_.SetActive(false);
        buttonActivePT_.SetActive(false);

        button.SetActive(true);
    }

    public void OnClickButtonEN()
    {
        selectEN_ = true;
        selectPT_ = false;
    }

    public void OnClickButtonPT()
    {
        selectPT_ = true;
        selectEN_ = false;
    }

    #endregion
}

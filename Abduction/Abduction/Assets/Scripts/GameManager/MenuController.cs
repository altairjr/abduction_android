using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject startMenu_;
    [SerializeField] private GameObject settingtMenu_;

    [Header("Audio Clip In Menu")]
    [SerializeField] private AudioClip audioClip_;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource_;

    [Header("Language EN")]
    [SerializeField] private GameObject language_en_menuSettings_;
    [SerializeField] private GameObject language_en_menuStart_;
    [SerializeField] private GameObject buttonActiveEN_;
    private bool selectEN_;

    [Header("Language PT")]
    [SerializeField] private GameObject language_pt_menuSettings_;
    [SerializeField] private GameObject language_pt_menuStart_;
    [SerializeField] private GameObject buttonActivePT_;
    private bool selectPT_;

    private GameObject menuActive_;

    public static MenuController Instancia { get; set; }

    private void Awake()
    {
        ChangeMenu(startMenu_);;
        ChangeButton(buttonActiveEN_);
        ChangeLanguageSettings(language_en_menuSettings_);
        ChangeLanguageStart(language_en_menuStart_);
    }

    private void Update()
    {
        SetLanguage();
    }

    private void ChangeMenu(GameObject menu)
    {
        startMenu_.SetActive(false);
        settingtMenu_.SetActive(false);

        menu.SetActive(true);
    }

    private void SetLanguage()
    {
        if (selectEN_)
        {
            ChangeLanguageSettings(language_en_menuSettings_);
            ChangeLanguageStart(language_en_menuStart_);
            ChangeButton(buttonActiveEN_);
        }


        if (selectPT_)
        {
            ChangeLanguageSettings(language_pt_menuSettings_);
            ChangeLanguageStart(language_pt_menuStart_);
            ChangeButton(buttonActivePT_);
        }
    }
    private void ChangeButton(GameObject button)
    {
        buttonActiveEN_.SetActive(false);
        buttonActivePT_.SetActive(false);

        button.SetActive(true);
    }

    private void ChangeLanguageSettings(GameObject language)
    {
        language_en_menuSettings_.SetActive(false);
        language_pt_menuSettings_.SetActive(false);

        language.SetActive(true);
    }

    private void ChangeLanguageStart(GameObject language)
    {
        language_en_menuStart_.SetActive(false);
        language_pt_menuStart_.SetActive(false);

        language.SetActive(true);
    }

    public void EN()
    {
        selectEN_ = true;
        selectPT_ = false;
    }

    public void PT()
    {
        selectPT_ = true;
        selectEN_ = false;
    }

    public void PlayAudioClip()
    {
        audioSource_.clip = audioClip_;
        audioSource_.Play();
    }
}

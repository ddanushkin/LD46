using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text shurikenNumLabel;
    [SerializeField] private Text HatNumLabel;
    [SerializeField] private Image shurikenFront;
    [SerializeField] private AudioClip shurikenIsReadyAudioClip;
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup mainGameUI;
    [SerializeField] private CanvasGroup mainMenuUI;
    
    private CanvasGroupFader mainGameUIFader;
    private CanvasGroupFader mainMenuUIFader;
    public static UIController Instance;

    void Awake() {
        Instance = this;
    }

    private int _shurikenNum;
    private int _hatNum = 0;

    private void Start()
    {
        mainMenuUI.alpha = 1f;
        mainMenuUIFader = mainMenuUI.GetComponent<CanvasGroupFader>();
        mainGameUI.alpha = 0f;
        mainGameUIFader = mainGameUI.GetComponent<CanvasGroupFader>();
    }

    public void SetSettings(float max, float current)
    {
        shurikenFront.fillAmount = 1f - (current / max);
        if (shurikenFront.fillAmount >= 1f)
            SoundManager.PlaySound(shurikenIsReadyAudioClip);
    }
    
    public void ChangeShurikenNum(int num)
    {
        string s;
        _shurikenNum = num;
        s = _shurikenNum.ToString();
        shurikenNumLabel.text = s;
    }

    public void ChangeHatNum(int num)
    {
        string s;
        _hatNum += num;
        s = _hatNum.ToString();
        HatNumLabel.text = s;
    }

    public void HideText()
    {
        _button.GetComponentInChildren<Text>().text = "";
    }

    public void ShowMainGameUI()
    {
        mainGameUIFader.FadeIn();
    }

    public void HideMainGameUI()
    {
        mainGameUIFader.FadeOut();
    }
 
    public void ShowMainMenuUI()
    {
        mainMenuUIFader.FadeIn();
    }
    
    public void HideMainMenuUI()
    {
        mainMenuUIFader.FadeOut();
    }
}

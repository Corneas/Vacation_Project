using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [Header("Menu")]
    private UIDocument uiDocument = null;
    private Button playButton = null;
    private Button settingButton = null;
    private Button exitButton = null;

    [SerializeField]
    private VisualTreeAsset settingsButtonsTemplate = null;
    private VisualElement settingsButtons = null;

    private VisualElement buttonsWrapper;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        playButton = uiDocument.rootVisualElement.Q<Button>("PlayButton");
        settingButton = uiDocument.rootVisualElement.Q<Button>("SettingButton");
        exitButton = uiDocument.rootVisualElement.Q<Button>("ExitButton");

        buttonsWrapper = uiDocument.rootVisualElement.Q<VisualElement>("Buttons");

        playButton.clicked += PlayButtonOnClicked;
        settingButton.clicked += SettingsButtonOnClicked;

        if(settingsButtonsTemplate != null)
            settingsButtons = settingsButtonsTemplate.CloneTree();
        else
            Debug.Log($"settingsButtonsTemplate is null!");      

        var backButton = settingsButtons.Q<Button>("BackButton");
        backButton.clicked += BackButtonOnClicked;
        
    }

    private void PlayButtonOnClicked()
    {
        Debug.Log("Play");
    }

    private void SettingsButtonOnClicked()
    {
        buttonsWrapper.Clear();
        buttonsWrapper.Add(settingsButtons);
    }

    private void BackButtonOnClicked()
    {
        buttonsWrapper.Clear();
        buttonsWrapper.Add(playButton);
        buttonsWrapper.Add(settingButton);
        buttonsWrapper.Add(exitButton);
    }
}

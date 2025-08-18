using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIOption : MonoBehaviour
{
    public enum OptionType
    {
        Menu,
        Retry,
        Exit
    }

    public Slider BGMSlider;
    public Slider SESlider;
    public OptionType type;

    public GameObject MessageBox;
    public Text messageText;

    void Start()
    {
		BGMSlider.value = Managers.Sound.GetBGMVol();
		SESlider.value = Managers.Sound.GetEffectVol();

		BGMSlider.onValueChanged.AddListener(Managers.Sound.SetBGMVol);
		SESlider.onValueChanged.AddListener(Managers.Sound.SetEffectVol);

        MessageBox.SetActive(false);

	}

    public void ButtonOptions(string _type)
    {
        MessageBox.SetActive(true);
        switch(_type)
        {
            case "Home":
                type = OptionType.Menu;
                messageText.text = "GO TO MENU?";
                break;
            case "Retry":
                type = OptionType.Retry;
                messageText.text = "RETRY?";
                break;
            case "Exit":
                type = OptionType.Exit;
                messageText.text = "GAME EXIT?";
                break;
        }

    }

    public void MessageBoxYesButton()
    {
        SceneMain main = Managers.Scene.CurScene.GetComponent<SceneMain>();
        switch(type)
        {
            case OptionType.Menu:
                Managers.TargetScene = Define.SceneType.MenuScene;
                main.ChangeScene();
                break;
            case OptionType.Retry:
				Managers.TargetScene = Define.SceneType.MainScene;
				main.ChangeScene();
				break;
            case OptionType.Exit:
                main.GameExit();
                break;
        }
    }
}

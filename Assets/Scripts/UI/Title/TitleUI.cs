using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public GameObject TitleTextPanel;

    public Slider BGMSlider;
    public Slider SESlider;

    void Start()
    {
        TitleTextPanel.SetActive(true);
	
        BGMSlider.value = Managers.Sound.GetBGMVol();
		SESlider.value = Managers.Sound.GetEffectVol();

		BGMSlider.onValueChanged.AddListener(Managers.Sound.SetBGMVol);
		SESlider.onValueChanged.AddListener(Managers.Sound.SetEffectVol);


	}

	public void GameExit()
    {
        Application.Quit();
    }

    public void CallMenuButtonSound()
    {
        Managers.Sound.Play("Effect/MenuButton");
    }
    public void DataResetButton()
    {
        Managers.GData.Clear();
        Managers.TargetScene = Define.SceneType.MenuScene;
		Managers.Scene.CurScene.GetComponent<SceneMenu>().SceneChanage();
	}
}

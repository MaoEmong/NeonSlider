using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMenu : SceneBase
{
    public Image BlackImage;


    public override void Clear(){}

    protected override void Start()
    {
		base.Start();
		Managers.Scene.CurScene = this;

        BlackImage.gameObject.SetActive(true);
        StartCoroutine(MyTools.ImageFadeOut(BlackImage, 0.3f));
        Managers.CallWaitForSeconds(0.3f, () => { BlackImage.gameObject.SetActive(false); });

        Managers.Sound.Play("BGM/MenuBGM", Define.Sound.Bgm);
	}

    public void SceneChanage()
    {
        BlackImage.gameObject.SetActive(true);
        StartCoroutine(MyTools.ImageFadeIn(BlackImage, 0.3f));
        Managers.CallWaitForSeconds(0.3f, () => { Managers.Scene.LoadScene(Define.SceneType.LoadScene); });
    }

}

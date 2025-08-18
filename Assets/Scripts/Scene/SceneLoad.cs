using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : SceneBase
{
    public Text LoadText;
    public string LoadingString = "LOAD";
    public string endLoadString = "SUCCESS!";
    bool isEnd = false;

    public override void Clear(){}

    protected override void Start()
    {
        base.Start();
        type = Define.SceneType.LoadScene;
        Managers.Scene.CurScene = this;
		Managers.GData.SaveData();
		Managers.Sound.Clear();

		StartCoroutine(LoadTextAction());
		StartCoroutine(LoadSceneAsync());

	}


	IEnumerator LoadTextAction()
	{
		int count = 0;
		LoadText.text = LoadingString;

		while (!isEnd)
		{
			yield return new WaitForSeconds(0.3f);
			LoadText.text = $"{LoadText.text}.";

			if (count > 3)
			{
				count = 0;
				LoadText.text = LoadingString;
			}

			count++;
		}
		LoadText.text = endLoadString;
	}

	IEnumerator LoadSceneAsync()
	{
		AsyncOperation op = SceneManager.LoadSceneAsync(Managers.TargetScene.ToString());
		op.allowSceneActivation = false;
		yield return new WaitForSeconds(1.0f);
		while (!op.isDone)
		{
			yield return new WaitForSeconds(0.1f);
			// 씬전환이 거의 끝나갈때 잠시 대기
			if (op.progress >= 0.9f)
			{
				isEnd = true;
				// 잠시 대기
				yield return new WaitForSeconds(0.8f);
				op.allowSceneActivation = true;
				yield break;
			}
		}
	}

}

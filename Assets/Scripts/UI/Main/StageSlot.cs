using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlot : MonoBehaviour
{
    public Text StageNumber;
    public Image Clear;
    public Image NonClear;
    
    public int StageNumberIndex;
    bool isClose = false;

    public GameObject StageMessage;
    public GameObject OutLine;

    public void Init(int number, GameObject message)
    {
        StageNumberIndex = number;
        StageMessage = message;

        StageNumber.text = $"Stage {StageNumberIndex + 1}";
		if (Managers.GData.Options.ClearData[StageNumberIndex].isCleared)
		{
			Clear.gameObject.SetActive(true);
            OutLine.SetActive(true);
		}
		else
		{
			Clear.gameObject.SetActive(false);
            OutLine.SetActive(false);
		}

		if (StageNumberIndex == 0)
        {
            isClose = false;
        }
        else
        {
            if (!Managers.GData.Options.ClearData[StageNumberIndex - 1].isCleared)
            {
                isClose = true;
            }
            else
            {
                isClose = false;
            }
        }

    }

    public void CallButton()
    {
        if(isClose)
        {
            StageMessage.SetActive(true);
            Managers.Sound.Play("Effect/MenuFail");

            Managers.CallWaitForSeconds(0.5f, () => 
            {
                StageMessage.SetActive(false);
            });
        }

        else
        {
            Managers.SelectStage = StageNumberIndex;
            Managers.Sound.Play("Effect/MenuButton");
            Managers.TargetScene = Define.SceneType.MainScene;
            Managers.Scene.CurScene.GetComponent<SceneMenu>().SceneChanage();
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMain : SceneBase
{
    public override void Clear(){}

    public Transform TileParant;

    public GameObject Player;

    public PlayerController player;

    public MainUI mainUI;

    public Image BlackImage;

    public GameObject ClearPanel;

    public int StarCount = 0;

    public BGScroll Bg;

    protected override void Start()
    {
        base.Start();
        Managers.Scene.CurScene = this;
        Managers.StarCount = 0;

		BlackImage.gameObject.SetActive(true);

        StageNumber = Managers.SelectStage;

		GameData.StageData stageData = Managers.GData.StageDataList[StageNumber];
        Debug.Log($"Size{stageData.MapData.GetLength(0)}X{stageData.MapData.GetLength(1)}");

		Managers.Sound.Play("BGM/MainBGM", Define.Sound.Bgm);

		for (int i = 0; i < stageData.MapData.GetLength(0); i++)
        {
            for (int j = 0; j < stageData.MapData.GetLength(1); j++)
            {
                GameObject obj = null;
                switch (stageData.MapData[i,j])
                {
                    case 0:
						break;
                    case 1:
						obj = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("prefab/BlueTile"));
						break;
                    case 2:
						obj = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/RedTile"));
						break;
                    case 3:
                        obj = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("prefab/GreenTile"));
                        if (Player == null)
                        {
                            Player = Managers.Resource.Instantiate("Player");
                            Player.name = "Player";
                        }
                        Player.transform.position = new Vector3(j * 2, i * -2, 0);
                        player = Player.GetComponent<PlayerController>();
                        mainUI.Player = player;
                        Bg.player = player;
                        break;
                    case 4:
                        obj = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/StarTile"));
                        StarCount++;
                        break;
                    case 5:
                        obj = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("prefab/PinkTile"));
                        break;
                }
                if (obj == null)
                    continue;
                else
                {
                    obj.transform.parent = TileParant;
                    obj.transform.localPosition = new Vector2(j * 2, i * -2);
                }
                
            }
        }

        Vector2 center = new Vector2((stageData.MapData.GetLength(1) - 1),-(stageData.MapData.GetLength(0) - 1));
        Vector2 mapsize = new Vector2(stageData.MapData.GetLength(1)+10, stageData.MapData.GetLength(0)+10);

        Camera.main.transform.GetComponent<CameraController>().Init(center, mapsize);

        StartCoroutine(MyTools.ImageFadeOut(BlackImage, 0.3f));
        Managers.CallWaitForSeconds(0.3f, () => { BlackImage.gameObject.SetActive(false); });
        ClearPanel.SetActive(false);

	}

    public void IsClearStage()
    {
        bool isClearStage = false;

        if(Managers.StarCount == StarCount)
        {
            isClearStage = true;
			Managers.Sound.Play("Effect/StageClear");
		}
        else
        {

			Managers.Sound.Play("Effect/StageFail");
		}

        ClearPanel.SetActive(true);
        ClearPanel.GetComponent<ClearPanel>().Init(StageNumber, isClearStage);
    }

    public void ChangeScene()
    {
        BlackImage.gameObject.SetActive(true);
        StartCoroutine(MyTools.ImageFadeIn(BlackImage, 0.3f));
        Managers.CallWaitForSeconds(0.3f, () => { Managers.Scene.LoadScene(Define.SceneType.LoadScene); });
        
    }

    public void GameExit()
    {
		BlackImage.gameObject.SetActive(true);
		StartCoroutine(MyTools.ImageFadeIn(BlackImage, 0.3f));
		Managers.CallWaitForSeconds(0.3f, () => { Application.Quit(); });

	}
}

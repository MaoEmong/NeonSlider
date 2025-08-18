using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 클리어판정 확인 UI
public class ClearPanel : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{
    public Text MoveCount;		// 이동횟수 표기
	public int StageIndex;		// 현재 스테이지 넘버
	public Text ClearText;		// 클리어 유무 표기
	bool isClearStage = false;	// 클리어 판정

	// 클리어 판정과 스테이지 넘버 받아오기
	public void Init(int stageindex, bool isClear)
	{
		isClearStage = isClear;
		// 클리어 판정에 따른 표기
		if (isClear) { ClearText.text = "STAGE CLEAR!"; }
		else { ClearText.text = "STAGE FAIL!"; }

		StageIndex = stageindex;
		int moveCount = 0;
		// 플레이어 데이터를 찾아 이동횟수 표기
		PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		if (player != null) { moveCount = player.moveCount; }
		else moveCount = -1;

		MoveCount.text = $"MOVE {moveCount}";
	}
	// IPointerDownHandler
	// 화면 터치 시작 (기능없음)
	public void OnPointerDown(PointerEventData eventData)
	{
	}
	// IPointerUpHandler
	// 화면 터치 끝
	public void OnPointerUp(PointerEventData eventData)
	{
		// 이동할 씬 선택
		Managers.TargetScene = Define.SceneType.MenuScene;
		// 현재 스테이지의 클리어 판정
		if (isClearStage) { Managers.GData.Options.ClearData[StageIndex].isCleared = true; }
		else { Managers.GData.Options.ClearData[StageIndex].isCleared = false; }
		// 씬 전환
		Managers.Scene.CurScene.GetComponent<SceneMain>().ChangeScene();
	}

}

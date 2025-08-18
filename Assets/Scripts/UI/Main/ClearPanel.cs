using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Ŭ�������� Ȯ�� UI
public class ClearPanel : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{
    public Text MoveCount;		// �̵�Ƚ�� ǥ��
	public int StageIndex;		// ���� �������� �ѹ�
	public Text ClearText;		// Ŭ���� ���� ǥ��
	bool isClearStage = false;	// Ŭ���� ����

	// Ŭ���� ������ �������� �ѹ� �޾ƿ���
	public void Init(int stageindex, bool isClear)
	{
		isClearStage = isClear;
		// Ŭ���� ������ ���� ǥ��
		if (isClear) { ClearText.text = "STAGE CLEAR!"; }
		else { ClearText.text = "STAGE FAIL!"; }

		StageIndex = stageindex;
		int moveCount = 0;
		// �÷��̾� �����͸� ã�� �̵�Ƚ�� ǥ��
		PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		if (player != null) { moveCount = player.moveCount; }
		else moveCount = -1;

		MoveCount.text = $"MOVE {moveCount}";
	}
	// IPointerDownHandler
	// ȭ�� ��ġ ���� (��ɾ���)
	public void OnPointerDown(PointerEventData eventData)
	{
	}
	// IPointerUpHandler
	// ȭ�� ��ġ ��
	public void OnPointerUp(PointerEventData eventData)
	{
		// �̵��� �� ����
		Managers.TargetScene = Define.SceneType.MenuScene;
		// ���� ���������� Ŭ���� ����
		if (isClearStage) { Managers.GData.Options.ClearData[StageIndex].isCleared = true; }
		else { Managers.GData.Options.ClearData[StageIndex].isCleared = false; }
		// �� ��ȯ
		Managers.Scene.CurScene.GetComponent<SceneMain>().ChangeScene();
	}

}

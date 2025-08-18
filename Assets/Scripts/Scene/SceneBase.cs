using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 씬의 기본 베이스 스크립트
public abstract class SceneBase : MonoBehaviour
{
    // 현재 씬의 정보
    public Define.SceneType type = Define.SceneType.Unknown;
    public int StageNumber = -1;

    void Awake()
    {
        Init();
    }

    protected virtual void Start()
    {
        Managers.GData.SaveData();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
		{
            GameObject go = new GameObject() { name = "EventSystem" };
            go.AddComponent<EventSystem>();
		}
        // 시간정지 해제
        Managers.TimePlay();
    }

    public abstract void Clear();


}

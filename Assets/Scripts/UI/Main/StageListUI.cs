using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageListUI : MonoBehaviour
{
    public Transform Content;
    public GameObject StageMessage;


    void Start()
    {
        for(int i = 0; i < Managers.GData.StageDataList.Count; i++)
        {
            GameObject stage = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/StageButton"));
            stage.transform.parent = Content;
            stage.transform.rotation = Quaternion.identity;
            stage.transform.localScale = Vector3.one;
            stage.GetComponent<StageSlot>().Init(i,StageMessage);
        }
        StageMessage.SetActive(false);
    }

}

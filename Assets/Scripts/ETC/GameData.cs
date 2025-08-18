using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// 게임데이터 관리 / 매니저 스크립트에서 관리
public class GameData
{
	/*
	 // 20 X 20 기본 타일
	 "MapData": [
      [ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 ],
      [ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ]
    ]
	 */
	public class StageData
	{
		// 0 = 길, 1 = 벽, 2 = 도착지점, 3 = 시작지점, 4 = 스타타일, 5 = 깨지는타일
		// 도착지점은 하나만 존재(플레이어 생성)
		public int StageNumber = 0;
		public int[,] MapData;
	
		public StageData()
		{
			MapData = new int[10,10];
			for(int i = 0; i < 10; i++)
			{
				for(int j = 0; j < 10; j++)
				{
					MapData[i,j] = 0;
				}
			}
		}

		public StageData(int[,] data)
		{
			MapData = data;
		}

	}

	public class GameOption
	{
		// 사운드 옵션
		public float BGMVol = 1;
		public float SEVol = 1;

		// 스테이지 옵션
		public class StageClearData
		{
			public int StageNumber;
			public bool isCleared;

			public StageClearData(int number, bool clear) { StageNumber = number; isCleared = clear; }

		}

		public List<StageClearData> ClearData = new();
	}


	public List<StageData> StageDataList = new();
	public GameOption Options = new();

	// 최초 초기화
	public void Init()
	{
		
		// 플레이어 데이터 받아오기
		string Path;
#if UNITY_EDITOR
		Path = Application.dataPath;
#else
		Path = Application.persistentDataPath;
#endif

		// 맵 데이터 가져오기
		/*
		if(File.Exists($"{Path}/JsonData/StageDataJson.json"))
		{
			Debug.Log("File is Find");
			StageDataList = Managers.Json.ImportJsonData<List<StageData>>($"JsonData", "StageDataJson");
			Debug.Log("Load File");
		}
		else
		{
			Debug.Log("File is NotFound");
			StageData data = new();
			StageDataList.Add(data);
			string stagedata = Managers.Json.ObjectToJson(StageDataList);
			Managers.Json.ExportJsonData("JsonData", "StageDataJson", stagedata);
		}
		*/


		StageDataList = Managers.Json.ImportReadOnlyJsonData<List<StageData>>("StageDataJson");
		
		if (File.Exists($"{Path}/JsonData/OptionJson.json"))
		{
			Debug.Log("File is Find");
			Options = Managers.Json.ImportJsonData<GameOption>($"JsonData", "OptionJson");
			Debug.Log("Load File");

			if(Options.ClearData.Count < StageDataList.Count)
			{
				for(int i = Options.ClearData.Count; i < StageDataList.Count; i++)
				{
					Options.ClearData.Add(new GameOption.StageClearData(i, false));
				}
			}


		}
		else
		{
			Debug.Log("File is NotFound");
			string optionData = Managers.Json.ObjectToJson(Options);
			Managers.Json.ExportJsonData("JsonData", "OptionJson", optionData);
			for (int i = 0; i < StageDataList.Count; i++)
			{
				Options.ClearData.Add(new GameOption.StageClearData(i, false));
			}
		}

	}
	// 게임 데이터 저장
	public void SaveData()
	{
		Debug.Log($"BGM{Options.BGMVol} / SE{Options.SEVol}");
		string optionData = Managers.Json.ObjectToJson(Options);
		Managers.Json.ExportJsonData("JsonData", "OptionJson", optionData);

	}
	// 게임 데이터 삭제
	public void Clear()
	{
		for(int i = 0; i < Options.ClearData.Count; i++)
		{
			Options.ClearData[i].isCleared = false;
		}
		SaveData();
	}
}

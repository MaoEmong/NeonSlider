using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// ���ӵ����� ���� / �Ŵ��� ��ũ��Ʈ���� ����
public class GameData
{
	/*
	 // 20 X 20 �⺻ Ÿ��
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
		// 0 = ��, 1 = ��, 2 = ��������, 3 = ��������, 4 = ��ŸŸ��, 5 = ������Ÿ��
		// ���������� �ϳ��� ����(�÷��̾� ����)
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
		// ���� �ɼ�
		public float BGMVol = 1;
		public float SEVol = 1;

		// �������� �ɼ�
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

	// ���� �ʱ�ȭ
	public void Init()
	{
		
		// �÷��̾� ������ �޾ƿ���
		string Path;
#if UNITY_EDITOR
		Path = Application.dataPath;
#else
		Path = Application.persistentDataPath;
#endif

		// �� ������ ��������
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
	// ���� ������ ����
	public void SaveData()
	{
		Debug.Log($"BGM{Options.BGMVol} / SE{Options.SEVol}");
		string optionData = Managers.Json.ObjectToJson(Options);
		Managers.Json.ExportJsonData("JsonData", "OptionJson", optionData);

	}
	// ���� ������ ����
	public void Clear()
	{
		for(int i = 0; i < Options.ClearData.Count; i++)
		{
			Options.ClearData[i].isCleared = false;
		}
		SaveData();
	}
}

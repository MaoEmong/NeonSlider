using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 기타 등등 enum값
public class Define
{
	// 사운드 종류
	public enum Sound
	{
		Bgm,
		Effect,
		MaxCount
	}
	// 씬 종류
	public enum SceneType
	{
		Unknown,
		MenuScene,
		LoadScene,
		MainScene

	}
}
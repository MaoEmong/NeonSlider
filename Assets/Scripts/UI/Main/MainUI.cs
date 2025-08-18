using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Text MoveCount;
    public PlayerController Player;

    void Update()
    {
        SetText();
    }

    void SetText()
    {
        if(Player != null)
        {
            MoveCount.text = $"MOVE {Player.moveCount}";
        }
    }

    public void CallMenuSound()
    {
        Managers.Sound.Play("Effect/MenuButton");
    }

}

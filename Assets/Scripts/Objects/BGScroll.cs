using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public  PlayerController player;

    SpriteRenderer BGSprite;

    Color DirColor;

	private void Start()
	{
		BGSprite = GetComponent<SpriteRenderer>();

        Color WhiteCol = Color.white;
        Color TargetCol = new Color(0.3f, 0.3f, 0.3f);
		DirColor = WhiteCol - TargetCol;
        

	}

	void Update()
    {
        ScrollBG();
        BreathColor();
    }

    void ScrollBG()
    {
        if (player == null) return;

        float mX = 0;
        float mY = 0;

        switch(player.moveVector)
        {
            case PlayerController.MoveVector.Up:
                mY = -1;
                break;
            case PlayerController.MoveVector.Down:
                mY = 1;
                break;
            case PlayerController.MoveVector.Left:
                mX = 1;
                break;
            case PlayerController.MoveVector.Right:
                mX -= 1;
                break;
        }

        Vector3 MoveVec = new Vector3(mX, mY,0);

        transform.localPosition += MoveVec * player.moveSpeed * 0.01f * Time.deltaTime;
    }

    void BreathColor()
    {
        if(BGSprite.color.r >= 1.0f || BGSprite.color.r <= 0.3f)
        {
            DirColor *= -1;
        }

        BGSprite.color += DirColor * 0.3f * Time.deltaTime;
    }
}

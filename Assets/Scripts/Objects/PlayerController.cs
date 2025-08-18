using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum MoveVector
    {
        Idle,
        Up,
        Down, 
        Left, 
        Right   
    
    }


    public float moveSpeed = 3.0f;
    public MoveVector moveVector = MoveVector.Idle;
    public bool isMove = false;
    public bool isHit = false;
    public bool isClear = false;
    public float MoveX = 0;
    public float MoveY = 0;

    public Text playerText;

    public CameraController Cam;

    public int moveCount = 0;


    //============ 터치
    Vector2 touchStartPos;
    Vector2 touchEndPos;
    Vector2 touchDir;
    float swipeSensitive = 0.3f;

    void Start()
    {
        playerText.text = "";

		Cam = Camera.main.transform.GetComponent<CameraController>();
        Cam.player = gameObject;

    }

    void Update()
    {
        if(isClear)
        {
            return;
        }

        PlayerMovement();
    }

    void PlayerMovement()
    {
        if(isMove)
        {
            if (!isHit)
            {
                Vector3 moveVec = new Vector3(MoveX, MoveY, 0);
                moveVec.Normalize();

                transform.position += moveVec * moveSpeed * Time.deltaTime;
            }
            return;
        }

        if(moveVector == MoveVector.Idle)
        {
            //===============키입력=================
            if (Input.GetKeyDown(KeyCode.W)) { moveVector = MoveVector.Up; }
            else if(Input.GetKeyDown(KeyCode.S)) {moveVector = MoveVector.Down; }
            else if(Input.GetKeyDown(KeyCode.D)) {moveVector = MoveVector.Right; }
            else if(Input.GetKeyDown(KeyCode.A)) { moveVector = MoveVector.Left; }

            //=============터치 입력==============
            // 터치입력이 들어왔을 때
            if(Input.touchCount > 0)
            {
                // 첫번째 터치값 가져옴
                Touch touch = Input.GetTouch(0);

                // 터치 상태가 시작됬을때
                if(touch.phase == TouchPhase.Began)
                {
                    // 터치 시작 값 받기
                    touchStartPos = touch.position;
                }
                // 터치상태가 끝났을 때
                else if(touch.phase == TouchPhase.Ended)
                {
                    // 터치 끝난 값 받기
                    touchEndPos = touch.position;
                    // 시작값과 끝난값으로 이동벡터 구하기
                    touchDir = touchEndPos - touchStartPos;
                    // 이동 벡터의 x,y 값 절대값으로 변환
                    float AbsX = Mathf.Abs(touchDir.x);
                    float AbsY = Mathf.Abs(touchDir.y);
                    // 두가지 이동 절대값 중 하나라도 터치 민감도값을 넘어간다면
                    if (AbsX > swipeSensitive || AbsY > swipeSensitive)
                    {
                        // 두 절대값을 비교하여 상하/좌우 상태 비교
                        // X값이 클때 (좌우)
                        if(AbsX > AbsY)
                        {
                            // 이동벡터의 x값이 양수인지 음수인지에 따라 좌/우 비교
                            if (touchDir.x > 0){ moveVector = MoveVector.Right;}
                            else { moveVector = MoveVector.Left; }
						}
                        // Y값이 클때 (상하)
                        else
                        {
                            // 이동벡터의 y값이 양수인지 음수인지에 따라 상/하 비교
                            if(touchDir.y>0) { moveVector = MoveVector.Up; }
                            else { moveVector = MoveVector.Down; }
                        }
                    }
                }
            }


		}
        else if(moveVector != MoveVector.Idle) 
        {
            isMove = true;
            moveCount++;
            Managers.Sound.Play("Effect/PlayerMove");
        }

        switch(moveVector) 
        {
            case MoveVector.Idle:
				playerText.text = "";
				break;
            case MoveVector.Up:
                MoveX = 0;
                MoveY = 1;
                playerText.text = "↑";
                break;
            case MoveVector.Down:
				MoveX = 0;
				MoveY = -1;
                playerText.text = "↓";
				break;
            case MoveVector.Right:
				MoveX = 1;
				MoveY = 0;
                playerText.text = "→";
				break;
            case MoveVector.Left:
				MoveX = -1;
				MoveY = 0;
                playerText.text = "←";
				break;
        }

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Block")&&!isHit)
		{
            Vector2 Pos = collision.ClosestPoint(transform.position);
            GameObject hitParticle = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("prefab/HitParticle"));
            hitParticle.transform.position = Pos;

            Managers.Sound.Play("Effect/PlayerHit");
			isHit = true;
            Cam.CameraShake(0.3f);
            playerText.text = "!";
            StartCoroutine(ReturnMove(collision.transform.localPosition));
		}
        else if(collision.transform.CompareTag("Star"))
        {
            GameObject particle = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("prefab/StarParticle"));
            Managers.Sound.Play("Effect/GetStar");
            particle.transform.position = collision.transform.localPosition;
            Managers.Pool.Push(collision.gameObject);
            Managers.StarCount++;
        }
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.transform.CompareTag("Goal")&& !isMove && !isHit && !isClear)
        {
            isClear = true;
            Managers.Scene.CurScene.GetComponent<SceneMain>().IsClearStage();
        }
	}


	IEnumerator ReturnMove(Vector3 hitpos)
    {

        float mX = 0;
        float mY = 0;

        switch(moveVector) 
        {
			case MoveVector.Up:
				mX = hitpos.x;
                mY = hitpos.y - 2;
				break;
			case MoveVector.Down:
				mX = hitpos.x;
				mY = hitpos.y + 2;
				break;
			case MoveVector.Right:
				mX = hitpos.x - 2;
				mY = hitpos.y;
				break;
			case MoveVector.Left:
				mX = hitpos.x + 2;
				mY = hitpos.y;
				break;
		}
		moveVector = MoveVector.Idle;

		Vector3 TargetVec = new Vector3(mX, mY, 0);
        Vector3 moveVec = TargetVec - transform.position;
        moveVec = moveVec.normalized;
        float curTime = 0;
        float endTime = 0.3f;
        float distance = Vector2.Distance(transform.position, TargetVec);
        float speed = distance / endTime;
        Debug.Log($"{speed}");
        
        while(curTime < endTime)
        {
            yield return null;
            curTime += Time.deltaTime;
            transform.position += moveVec * speed * Time.deltaTime;
        }
        transform.position = TargetVec;
        playerText.text = "";
        isMove = false;
        isHit = false;
    }
}

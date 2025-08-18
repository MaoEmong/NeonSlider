using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // 카메라 기본값
    Vector3 cameraPos = new Vector3(0, 0, -20);

    // 카메라 움직임 속도
    [SerializeField]
    float moveSpeed = 5.0f;

    // 카메라 이동 제한 크기의 중심점
    [SerializeField]
    Vector2 center;
    // 카메라 이동 제한 크기
    [SerializeField]
    Vector2 mapSize;

    float height;
    float width;

    bool isShake = false;

    void Start()
    {
        // 카메라의 화면 크기 설정
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        
    }

    public void Init(Vector2 _center,Vector2 _mapsize)
    {
        center = _center;
        mapSize = _mapsize;
    }

    void LateUpdate()
    {
        if(player == null) { return; }

        CameraMove();
    }

    private void CameraMove()
	{
        // 카메라의 선형보간
        transform.position = Vector3.Lerp(transform.position, 
            player.transform.position + cameraPos, 
            moveSpeed * Time.deltaTime);

        // 카메라 가두기
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        if(isShake)
        {
            float randX = Random.Range(0.01f, 0.1f);
            float randY = Random.Range(0.01f, 0.1f);
            transform.position = new Vector3(clampX+randX, clampY+randY, -10f);
		}
        else
    		transform.position = new Vector3(clampX, clampY, -10f);
    }

    // 가두는 공간 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }

    public void CameraShake(float time)
    {
        if (isShake) return;

        isShake = true;
        Managers.CallWaitForSeconds(time, () => { isShake = false; });  

    }
}

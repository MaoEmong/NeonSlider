using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextUI : MonoBehaviour
{
    public Outline TextOutline;

    float textSpeed = 3f;
    Vector2 TargetVec = new Vector2(5.0f, 5.0f);
    Vector2 Dir;

    private void Start()
    {
        Dir = TargetVec - TextOutline.effectDistance;
        Dir = Dir.normalized;
    }

    void Update()
    {

        TextOutline.effectDistance += Dir * textSpeed * Time.deltaTime;
        if (TextOutline.effectDistance.x > TargetVec.x || TextOutline.effectDistance.x < 2f)
        {
            Dir *= -1;
        }
    }
}

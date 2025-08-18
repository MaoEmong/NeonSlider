using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScaleChange : MonoBehaviour
{
    public Image image;
    public float speed = 0.24f;


    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        image.rectTransform.localScale += Vector3.one * speed * Time.deltaTime;
        if(image.rectTransform.localScale.x > 1.2f)
        {
            image.rectTransform.localScale = Vector3.one;
        }
    }
}

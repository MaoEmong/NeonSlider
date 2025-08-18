using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaleChange : MonoBehaviour
{
	public float speed = 0.24f;

	void Update()
    {
		transform.localScale += Vector3.one * speed * Time.deltaTime;
		if (transform.localScale.x > 1.2f)
		{
			transform.localScale = Vector3.one;
		}

	}
}

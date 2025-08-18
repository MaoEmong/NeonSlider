using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkTile : MonoBehaviour
{
    public int Hp = 5;

    public List<GameObject> HpSprite;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Hp--;

            if(Hp <= 0)
            {
                transform.GetComponent<BoxCollider2D>().enabled = false;
            }

            SetSpriteActive();
            
        }
    }

    void SetSpriteActive()
    {
        for(int i = 0; i < HpSprite.Count; i++)
        {
            if(i >= Hp)
            {
                HpSprite[i].SetActive(false);
            }
        }
    }

}

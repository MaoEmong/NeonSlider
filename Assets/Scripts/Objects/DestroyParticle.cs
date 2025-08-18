using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    ParticleSystem particle;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Play();
    }

    private void Update()
    {
        if(particle.isStopped)
        {
            Managers.Pool.Push(gameObject);
        }
    }

}

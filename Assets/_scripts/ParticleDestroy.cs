using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private ParticleSystem particleSys;

	void Start ()
    {
        particleSys = GetComponent<ParticleSystem>();
	}
	
	void Update ()
    {
        if (!particleSys.isPlaying) Destroy(gameObject);
	}
}

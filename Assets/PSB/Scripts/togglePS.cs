using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togglePS : MonoBehaviour {

    private ParticleSystem[] particleSystems;

	// Use this for initialization
	void Start () {
        particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
	}

    public void toggleParticleSystem() {
        if (particleSystems[0].isPlaying)
            foreach (ParticleSystem particle in particleSystems)
                particle.Stop();
        else
            foreach (ParticleSystem particle in particleSystems)
                particle.Play();
    }
}

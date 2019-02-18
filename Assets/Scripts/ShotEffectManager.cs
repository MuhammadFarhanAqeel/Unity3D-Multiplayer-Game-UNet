using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEffectManager : MonoBehaviour {

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource gunAudio;
    [SerializeField] GameObject impactPrefab;


    ParticleSystem impactEffect;

    public void Initialize() {

        impactEffect = Instantiate(impactPrefab).GetComponent<ParticleSystem>();

    }

    public void PlayShotEffect() {
        muzzleFlash.Stop(true);
        muzzleFlash.Play(true);
        gunAudio.Stop();
        gunAudio.Play();

    }


    public void PlayImpactEffect(Vector3 impactPosition)
    {

        if (impactEffect)
        {
            
            impactEffect.transform.position = impactPosition;
            impactEffect.Stop();
            impactEffect.Play();
        }
    }
}

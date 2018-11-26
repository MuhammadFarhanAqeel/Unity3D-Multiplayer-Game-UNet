using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {


	public ParticleSystem _muzzleFlash;
	public AudioSource _gunAudio;
	public GameObject _impactPrefab;
	public Transform cameraTransform;

	ParticleSystem _impactEffect;







	// Use this for initialization
	void Start () {
		_impactEffect = Instantiate (_impactPrefab).GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) {
		
			_muzzleFlash.Stop ();
			_muzzleFlash.Play();

			_gunAudio.Stop ();
			_gunAudio.Play ();


			RaycastHit hit;
			Vector3 rayPos = cameraTransform.position + (1f * cameraTransform.forward);

			if (Physics.Raycast (rayPos, cameraTransform.forward, out hit, 50f)) {
			
				_impactEffect.transform.position = hit.point;
				_impactEffect.Stop ();
				_impactEffect.Play ();


				if (hit.transform.tag == "Player") {
				
					Destroy (hit.transform.gameObject);

				}

			}

		}
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneMaterialSwitcher : MonoBehaviour {



	public Material localCaneMaterial;
	public Material notLocalCaneMaterial;

	Renderer candyCaneRenderer;




	// Use this for initialization
	void Awake () {

		candyCaneRenderer = GetComponent<Renderer> ();


		SwitchMaterial (false);
	}
	
	void SwitchMaterial(bool isLocalPlayer){
		if (isLocalPlayer) {
			candyCaneRenderer.material = localCaneMaterial;
		} else {
			candyCaneRenderer.material = notLocalCaneMaterial;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkedPlayerScript : NetworkBehaviour 
{

	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
	public Camera fpsCamera;
	public AudioListener audioListener;
	public ShootingScript shootingScript;
	public CandyCaneMaterialSwitcher candyMaterialSwitcher;

	public override void OnStartLocalPlayer(){

		fpsController.enabled = true;
		fpsCamera.enabled = true;
		audioListener.enabled = true;
		shootingScript.enabled = true;
		candyMaterialSwitcher.SwitchMaterial (true);
		gameObject.name = "LOCAL Player";

		base.OnStartLocalPlayer ();
	}



	void ToggleRenderers(bool isAlive){
	
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();

		for (int i = 0; i < renderers.Length; i++) {
			renderers [i].enabled = isAlive;
					
		}

	}



	void ToggleControls(bool isAlive){
	
		fpsController.enabled = isAlive;
		shootingScript.enabled = isAlive;
		fpsCamera.cullingMask = ~fpsCamera.cullingMask;


	}

	[ClientRpc]
	public void RpcResolveHit(){

		ToggleRenderers (false);
		if (isLocalPlayer) {
		
			Transform spawn = NetworkManager.singleton.GetStartPosition ();

			transform.position = spawn.position;
			transform.rotation = spawn.rotation;

			ToggleControls (false);

		}

		Invoke ("Respawn", 2f);
	}


	void Respawn(){
		ToggleRenderers (true);
		if (isLocalPlayer) {
			ToggleControls (true);
		}
	}

}

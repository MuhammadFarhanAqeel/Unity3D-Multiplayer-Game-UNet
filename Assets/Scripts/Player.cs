using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;



[System.Serializable]
public class ToggleEvent : UnityEvent<bool> {
}



public class Player : NetworkBehaviour {

     
    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;

    GameObject MainCamera; 
    void Start() {
        MainCamera = Camera.main.gameObject;
        EnablePlayer();


    }



    void DisablePlayer() {
        if (isLocalPlayer)
            MainCamera.SetActive(true);

        onToggleShared.Invoke(false);

        if (isLocalPlayer)
        {
            onToggleLocal.Invoke(false);
        }
        else
        {
            onToggleRemote.Invoke(false);
        }
    }

    void EnablePlayer() {
        if (isLocalPlayer)
            MainCamera.SetActive(false);

        onToggleShared.Invoke(true);

        if (isLocalPlayer)
        {
            onToggleLocal.Invoke(true);
        }
        else {
            onToggleRemote.Invoke(true);
        }
    }

}

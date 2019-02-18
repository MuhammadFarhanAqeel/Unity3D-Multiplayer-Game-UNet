using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerShooting :  NetworkBehaviour{

    [SerializeField] float shotCoolDown = .03f;
    [SerializeField] Transform firePosition;
    [SerializeField] ShotEffectManager shotEffects;


    [SyncVar(hook = "OnScoreChanged")] int score;

    float ellapsedTime;
    bool canShoot;



    void Start() {
        shotEffects.Initialize();

        if (isLocalPlayer)
            canShoot = true;
    }

    [ServerCallback]
    void OnEnable()
    {

        score = 0;
    }

    void Update() {

        if (!canShoot)
            return;


        ellapsedTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && ellapsedTime >shotCoolDown) {
            ellapsedTime = 0;

            // Cmd ... cmd stands for a command that will run on the server
            CmdFireShot(firePosition.position,firePosition.forward);
            

        }
    }


    [Command]
    void CmdFireShot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red, 1f);

        bool result = Physics.Raycast(ray, out hit, 50);

        if (result)
        {
            // health stuff

            PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth>();
            if (enemy != null)
            {
                bool wasKillShot = enemy.TakeDamage();
                if (wasKillShot)
                    score++;
            }
        }

        RpcProcessShotEffects(result, hit.point);

    }



    [ClientRpc]
    void RpcProcessShotEffects(bool playImpact, Vector3 point) {

        shotEffects.PlayShotEffect();

        if (playImpact)
            shotEffects.PlayImpactEffect(point);

    }



    void OnScoreChanged(int value) {
        score = value;

        if (isLocalPlayer) {
            PlayerCanvas.canvas.SetKills(value);
                }
    }
}



using UnityEngine;
using UnityEngine.Networking;



public class PlayerHealth : NetworkBehaviour {



    [SerializeField] int maxHealth = 3;
    [SyncVar(hook = "OnHealthChanged")] int health;

    Player player;




    void Awake()
    {

        player = GetComponent<Player>();

    }


    [ServerCallback]
    void OnEnable() {
        health = maxHealth;
    }

    [ServerCallback]
    void Start()
    {
        health = maxHealth;
    }

    [Server]
    public bool TakeDamage() {

        bool died = false;

        if (health <= 0)
            return died;

        health--;

        died = health <= 0;


        RpcTakeDamage(died);
        return died;

    }

    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        if (isLocalPlayer) {
            PlayerCanvas.canvas.FlashDamageEffect();
        }
        if (died)
            player.Die();
    }

    void OnHealthChanged(int value)
    {
        health = value;
        if (isLocalPlayer) {
            PlayerCanvas.canvas.SetHealth(value);
        }
    } }

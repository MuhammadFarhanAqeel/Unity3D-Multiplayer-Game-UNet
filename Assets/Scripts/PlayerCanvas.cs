using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCanvas : MonoBehaviour {



    public static PlayerCanvas canvas;



    [Header("Component References")]
    [SerializeField] Image reticule;
    [SerializeField] UIFader damageImage;
    [SerializeField] Text gameStatusText;
    [SerializeField] Text healthValue;
    [SerializeField] Text killsValue;
    [SerializeField] Text logText;
    [SerializeField] AudioSource deathAudio;


    void Awake()
    {

        if (canvas == null)
            canvas = this;
        else if (canvas != null)
            Destroy(gameObject);

    }


    void Reset() {  
        reticule = GameObject.Find("Reticule").GetComponent<Image>();
        damageImage = GameObject.Find("DamagedFlash").GetComponent<UIFader>();
        gameStatusText = GameObject.Find("GameStatusText").GetComponent<Text>();
        healthValue = GameObject.Find("HealthValue").GetComponent<Text>();
        killsValue = GameObject.Find("KillsValue").GetComponent<Text>();
        logText = GameObject.Find("LogText").GetComponent<Text>();
        deathAudio = GameObject.Find("DeathAudio").GetComponent<AudioSource>();
    }


    public void Initialize() {
        reticule.enabled = true;
        gameStatusText.text = "";
    }

    public void HideReticule() {
        reticule.enabled = false;
    }

    public void FlashDamageEffect() {
        damageImage.Flash();
    }

    public void PlayDeathAudio() {
        if (!deathAudio.isPlaying) {

            deathAudio.Play();

        }

    }


    public void SetKills(int amount) {
        killsValue.text = amount.ToString();

    }


    public void SetHealth(int amount) {
        healthValue.text = amount.ToString();
    }

    public void WriteGameStatusText(string text) {

        gameStatusText.text = text.ToString();

    }


    public void WriteLogText(string text, float duration) {

        CancelInvoke();
        logText.text = text.ToString();
        Invoke("ClearLogText", duration);

    }

    void ClearLogText() {

        logText.text = "";

    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

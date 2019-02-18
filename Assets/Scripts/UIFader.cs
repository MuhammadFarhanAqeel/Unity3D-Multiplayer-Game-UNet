using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIFader : MonoBehaviour
{




    Image SplashImage;



    // Use this for initialization
    void Start()
    {
        SplashImage = GetComponent<Image>();
        SplashImage.enabled = false;
    }



    public void Flash()
    {


        if (!SplashImage.IsActive())
        {
            SplashImage.enabled = true;

            Invoke("DisableSplash", .05f);
        }
    }

    void DisableSplash()
    {
        SplashImage.enabled = false;
    }
}
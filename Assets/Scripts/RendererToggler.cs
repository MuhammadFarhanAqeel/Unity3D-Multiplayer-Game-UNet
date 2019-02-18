using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererToggler : MonoBehaviour
{




    [SerializeField] float turnOnDelay = .2f;
    [SerializeField] float turnOffDelay = 2.25f;
    [SerializeField] bool enabledOnLoaded = false;

    Renderer[] renderers;


    void Awake()
    {

        renderers = GetComponentsInChildren<Renderer>(true);
        if (enabledOnLoaded)
            EnableRenderers();
        else
            DisableRenderers();
    }

    public void ToggleRenderersDelayed(bool isOn)
    {

        if (isOn)
        {

            Invoke("EnableRenderers", turnOnDelay);

        }
        else
        {
            Invoke("DisableRenderers", turnOffDelay);
        }

    }

    void EnableRenderers()
    {

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = true;
        }



    }
    void DisableRenderers()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }
    }



    public void ChangeColor(Color newColor)
    {

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = newColor;
        }
    }
}
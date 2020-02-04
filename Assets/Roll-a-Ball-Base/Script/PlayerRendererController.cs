using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRendererController : MonoBehaviour
{

    private new Renderer renderer;
    public float animationTime = 0.8f;
    public float animationRate = 0.1f;
    public Color emissionColor = Color.white / 2;


    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void SetAnimation()
    {
        StartCoroutine(AnimationCoroutine());

    }
    private IEnumerator AnimationCoroutine()
    {
        renderer.material.EnableKeyword("_EMISSION");
        for (int i = 0; i < animationTime / animationRate; i++) {
            renderer.material.SetColor("_EmissionColor", emissionColor);
            yield return new WaitForSeconds(animationRate / 2);
            renderer.material.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(animationRate / 2);
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : MonoBehaviour {

    [SerializeField]
    Canvas gageUnitCanvas;
    [SerializeField]
    Image imageBack;
    [SerializeField]
    Image imageFoward;
    [SerializeField]
    float changeTime = 0.5f;

    private float fadeTime = 0.0f;

    // Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {
        fadeTime += Time.deltaTime;
        if (fadeTime >= changeTime) {
            fadeTime = changeTime;
            imageBack.rectTransform.localScale = imageFoward.rectTransform.localScale;
        }
        else {
            imageBack.rectTransform.localScale = Vector3.Lerp(imageBack.rectTransform.localScale, imageFoward.rectTransform.localScale, fadeTime / changeTime);
        }


    }
    public void SetGageUnit(int nGage)
    {
        GameObject unit =gageUnitCanvas.transform.GetChild(0).gameObject;
        for(int i = 0; i < nGage-1; i++) {
            GameObject instance= Instantiate(unit);
            instance.transform.SetParent(gageUnitCanvas.transform);
            instance.transform.localScale = Vector3.one;
        }
    }

    public void ChangeValue(float newValue)
    {
        fadeTime = 0;
        Vector3 localScale = new Vector3(newValue, 1, 0);
        imageFoward.rectTransform.localScale = localScale;
    }
}

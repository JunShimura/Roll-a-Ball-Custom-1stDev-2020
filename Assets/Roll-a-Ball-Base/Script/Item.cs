using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Item : MonoBehaviour
{
    public float destroyDelayTime = 1.0f;
    private AudioSource audioSource;
    private new Collider collider;
    private Animator animator;
    private new Renderer renderer;
    private Transform innerLight;

    private void Start()
    {
        animator = GetComponent<Animator>();
        innerLight = transform.GetChild(0);
        audioSource = GetComponent<AudioSource>();
        //collider = GetComponent<Collider>();
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();

    }

    // トリガーとの接触時に呼ばれるコールバック
    void OnTriggerEnter(Collider hit)
    {
        // 接触対象はPlayerタグですか？
        if (hit.CompareTag("Player")) {
            hit.gameObject.transform.GetComponentInChildren<PlayerRendererController>().SetAnimation();
            // このコンポーネントを持つGameObjectを破棄する
            audioSource.Play();
            collider.enabled = false;
            gameObject.transform.tag = "BrokenItem";
            animator.SetTrigger("broken");
            Destroy(renderer,destroyDelayTime);
            innerLight.gameObject.SetActive(true);
            Destroy(gameObject,destroyDelayTime);
        }
    }
}

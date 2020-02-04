using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;    // ターゲットへの参照
    private Vector3 offset;     // 相対座標

    private void Reset()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        Reset();
        //自分自身とtargetとの相対距離を求める
        offset = GetComponent<Transform>().position - target.position;
    }

    void Update()
    {
        // 自分自身の座標に、targetの座標に相対座標を足した値を設定する
        if (target != null) {
            Transform targetTransform = target.GetComponent<Transform>();
            transform.position = targetTransform.position + offset;
        }
    }
}

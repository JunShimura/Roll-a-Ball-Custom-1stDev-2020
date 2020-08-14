using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerWall : MonoBehaviour
{

    //オブジェクトと接触した時に呼ばれるコールバック
    private void OnCollisionEnter(Collision hit)
    {
        //接触したオブジェクトのタグが"Player"だった場合
        if (hit.gameObject.CompareTag("Player")) {
            hit.gameObject.GetComponent<PlayerController>().SetBroken();
        }
    }
}

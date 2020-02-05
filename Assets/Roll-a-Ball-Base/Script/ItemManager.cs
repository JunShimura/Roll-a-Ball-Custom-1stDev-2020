using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    List<GameObject> item = new List<GameObject>();
    GameController gameController;

    public int lastCount {
        get => item.Count;
    }
    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject tempItem in temp) {
            tempItem.transform.SetParent(this.transform);
            item.Add(tempItem);
        }
    }
    public int DeleteItem(GameObject lost)
    {
        item.Remove(lost);
        gameController.CatchItem(item.Count);
        return item.Count;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //Singlton
    static ItemManager _S = null;

    List<GameObject> item = new List<GameObject>();
    GameController gameController;

    public int lastCount {
        get => item.Count;
    }
    void Awake()
    {
        if (_S == null)
        {
            _S = this;
        }
        else
        {
            Debug.LogError("Duplicated ItemManager");
        }
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject tempItem in temp) {
            tempItem.transform.SetParent(this.transform);
            item.Add(tempItem);
        }
    }
    public int DeleteItem(GameObject lost)
    {
        item.Remove(lost);
        GameController.CatchItem(item.Count);
        return item.Count;
    }
}

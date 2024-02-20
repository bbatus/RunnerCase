using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public Transform endTransform; // EndTransform'un referansını tutacak.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            LoadRandomLevel();
        }
    }

    void LoadRandomLevel()
    {
        int randomIndex = Random.Range(1, 4); //3 level icin
        string tag = "Level" + randomIndex; 
        ObjectPooling.Instance.SpawnFromPool(tag, endTransform.position, Quaternion.identity);
    }

}

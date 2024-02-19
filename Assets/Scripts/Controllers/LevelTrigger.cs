using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public Transform endTransform; // EndTransform'un referansını tutacak.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadRandomLevel();
        }
    }

    void LoadRandomLevel()
    {
        int randomIndex = Random.Range(1, 4); // 10 level için 0'dan 9'a kadar rastgele bir index seçer.
        string tag = "Level" + randomIndex; // Tag'leriniz "Level0", "Level1", ..., "Level9" şeklinde olmalıdır.
        ObjectPooling.Instance.SpawnFromPool(tag, endTransform.position, Quaternion.identity); // Yeni level'ı belirli bir konumda aktifleştirir.
    }

}

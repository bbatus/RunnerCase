using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levelPrefabs; // Level prefab'larınızı buraya yerleştirin.
    private GameObject currentLevel; // Şu anki level parçası.
    public Transform levelSpawnPoint; // Yeni level parçalarının spawn olacağı nokta.

    void Start()
    {
        SpawnLevel(); // Oyun başladığında ilk level parçasını spawn et.
    }

    public void SpawnLevel()
    {
        // Eğer aktif bir level varsa, onu yok et.
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        // Rastgele bir level prefab'ı seç ve spawn noktasında oluştur.
        GameObject levelPrefab = levelPrefabs[Random.Range(0, levelPrefabs.Length)];
        currentLevel = Instantiate(levelPrefab, levelSpawnPoint.position, Quaternion.identity);

        // Level parçasının sonuna bir trigger ekleyerek oyuncunun sona ulaştığını algıla.
        // Bu örnekte trigger mekanizması kod olarak gösterilmemiştir. Bunun yerine, level prefab'ınızın sonuna
        // bir GameObject ekleyip, bu GameObject'e bir Collider (isTrigger seçili) ve bu script'e bağlı bir
        // script ekleyerek bu işlevselliği sağlayabilirsiniz.
    }

}
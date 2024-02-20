using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, GameObject> prefabDictionary; // Prefab'ları tutmak için yeni bir dictionary

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        prefabDictionary = new Dictionary<string, GameObject>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
            prefabDictionary.Add(pool.tag, pool.prefab); // Her tag için prefab'ı sakla
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.TryGetValue(tag, out Queue<GameObject> objectQueue))
        {
            Debug.LogWarning($"Pool with tag {tag} not found");
            return null;
        }

        GameObject objectToSpawn;
        if (objectQueue.Count == 0)
        {
            objectToSpawn = ExpandPool(tag, objectQueue);
        }
        else
        {
            objectToSpawn = objectQueue.Dequeue();
        }

        // Gerekirse objeyi resetle
        ResetObject(objectToSpawn);

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectQueue.Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private GameObject ExpandPool(string tag, Queue<GameObject> objectQueue)
    {
        if (!prefabDictionary.TryGetValue(tag, out GameObject prefab))
        {
            Debug.LogError($"No prefab found for tag {tag}");
            return null;
        }

        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);

        objectQueue.Enqueue(newObj);

        return newObj;
    }

    private void ResetObject(GameObject obj)
    {
        // Burada obje için gerekli reset işlemlerini gerçekleştir
        // Örneğin, Collectable objeleri resetle
        var collectables = obj.GetComponentsInChildren<CollectGold>(true);
        foreach (var collectable in collectables)
        {
            collectable.ResetGold();
        }
    }
}

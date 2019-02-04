using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                ObjectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.tag, ObjectPool);
        }
    }
    /// <summary>
    /// Destroy parameter to set all active pools false before every new spawn
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <param name="Destroy"></param>
    public void SpawnPool(string tag, Vector3 pos, Quaternion rot, bool Destroy = false)
    {
        if(Destroy)
        {
            foreach (var pool in pools)
            {
                foreach (var gameObj in PoolDictionary[pool.tag])
                {
                    gameObj.SetActive(false);
                }
            }
        }


        GameObject Obj = PoolDictionary[tag].Dequeue();
        Obj.SetActive(true);
        Obj.transform.position = pos;
        Obj.transform.rotation = rot;

        PoolDictionary[tag].Enqueue(Obj);
    }
}

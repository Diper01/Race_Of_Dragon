using UnityEngine;
using System.Collections.Generic;

namespace MaxTools
{
    public class GamePool
    {
        static Dictionary<GameObject, GamePool> pools =
           new Dictionary<GameObject, GamePool>();

        GameObject prefab = null;

        Queue<GameObject> poolQueue = null;

        public GamePool(GameObject prefab)
        {
            this.prefab = prefab;

            poolQueue = new Queue<GameObject>();
        }

        public void AddToPool(GameObject go)
        {
            if (!poolQueue.Contains(go))
            {
                go.SetActive(false);

                poolQueue.Enqueue(go);
            }
        }

        public GameObject GetFromPool()
        {
            if (poolQueue.Count > 0)
            {
                var go = poolQueue.Dequeue();

                go.SetActive(true);

                return go;
            }
            else
                return Object.Instantiate(prefab);
        }

        public bool GetFromPool(out GameObject go)
        {
            if (poolQueue.Count > 0)
            {
                go = poolQueue.Dequeue();

                go.SetActive(true);

                return true;
            }
            else
            {
                go = Object.Instantiate(prefab);

                return false;
            }
        }

        public void ClearAndDestroy()
        {
            foreach (var go in poolQueue)
            {
                Object.Destroy(go);
            }

            poolQueue.Clear();
        }

        public static GamePool GetPool(GameObject prefab)
        {
            if (!pools.TryGetValue(prefab, out var pool))
            {
                pool = new GamePool(prefab);

                pools[prefab] = pool;
            }

            return pool;
        }
    }
}

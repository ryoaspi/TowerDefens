using UnityEngine;
using System.Collections.Generic;


public class EnemyPoolManager : MonoBehaviour
{
    #region Publics
    
    public static EnemyPoolManager Instance { get; private set; }

    [System.Serializable]
    public class EnemyPool
    {
        public GameObject m_prefab;
        public int m_initialSize = 10;
        [HideInInspector] public Queue<GameObject> poolQueue = new Queue<GameObject>();
    }
    
    #endregion
    
    
    #region Api Unity
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return; 
        }
        Instance = this;
    }
    
    
    #endregion
    
    
    #region Main Method

    public GameObject GetEnemy(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!_enemiesPools.ContainsKey(prefab))
        {
            _enemiesPools[prefab] = new Queue<GameObject>();
        }

        GameObject enemy;

        if (_enemiesPools[prefab].Count > 0)
        {
            enemy = _enemiesPools[prefab].Dequeue();
            enemy.transform.SetPositionAndRotation(position, rotation);
            enemy.SetActive(true);
        }
        else
        {
            enemy = Instantiate(prefab, position, rotation);
            // 👇 Rend l'ennemi enfant de ce GameObject EnemyPoolManager
            enemy.transform.SetParent(transform);
        }

        return enemy;
    }

    public void ReturnToPool(GameObject enemy, GameObject prefab)
    {
        enemy.SetActive(false);
        enemy.transform.SetParent(transform); // Remet dans la hiérarchie du pool manager
        _enemiesPools[prefab].Enqueue(enemy);
    }
    
    #endregion
    
    
    #region Private And Protected

    [SerializeField] private EnemyPool[] _enemyPools;
    private Transform _poolParent;
    private Dictionary<GameObject, Queue<GameObject>> _enemiesPools = new();

    #endregion
}

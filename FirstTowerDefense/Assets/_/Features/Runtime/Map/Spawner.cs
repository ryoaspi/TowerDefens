using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Api Unity

    private void Start()
    {
        if (_waves.Length > 0)
        {
            _waveDelayTimer = _waves[0].m_waveDelay;
        }
    }

    private void Update()
    {
        if (_currentWaveIndex >= _waves.Length) return;

        if (!_waveInProgress)
        {
            _waveDelayTimer -= Time.deltaTime;
            if (_waveDelayTimer <= 0f)
            {
                StartWave();
            }
        }
        if (_spawningWaves) SpawnWave();
    }


    #endregion
    
    
    #region Utils
    
    private void StartWave()
    {
        _waveInProgress = true;
        _spawningWaves = true;
        _currentEnemyTypeIndex = 0;
        _spawnedCountForType = 0;
        _spawnTimer = 0f;
    }
    
    private void SpawnWave()
    {
        WaveSet currentWaveSet = _waves[_currentWaveIndex];
        WaveData currentWaveData = currentWaveSet.m_waves[_currentWaveIndex];
        
        
        if (_currentEnemyTypeIndex >= currentWaveData.m_enemiesInWave.Length)
        {
            _spawningWaves = false;
            _waveInProgress= false;
            _currentWaveIndex++;
            
            if (_currentWaveIndex < _waves.Length) 
                _waveDelayTimer = _waves[_currentWaveIndex].m_waveDelay;
            return;
        }

        EnemyWaveData currentEnemy = currentWaveData.m_enemiesInWave[_currentEnemyTypeIndex];
        
        _spawnTimer += Time.deltaTime;
        if (_spawnedCountForType < currentEnemy.m_count && _spawnTimer >= currentEnemy.m_spawnDelay)
        {
            GameObject enemyInstance = EnemyPoolManager.Instance.GetEnemy(currentEnemy.m_enemyPrefab,_spawnPoint.position, Quaternion.identity);
            
            // Bonus :Appliquer un multiplicateur de pv basé sur la vague
            float healthMultiplier = 1f + (_currentWaveIndex * 0.1f);
            EnemyMovement enemyScript = enemyInstance.GetComponent<EnemyMovement>();
            if (enemyScript != null)
            {
                enemyScript.ApplyHealthMultiplier(healthMultiplier);
            }
            _spawnedCountForType++;
            _spawnTimer = 0f;
        }

        if (_spawnedCountForType >= currentEnemy.m_count)
        {
            _currentEnemyTypeIndex++;
            _spawnedCountForType = 0;
            _spawnTimer = 0f;
        }
    }
    
    #endregion
    
    
    #region Private And Protected

    [SerializeField] private WaveSet[] _waves;
    [SerializeField] private Transform _spawnPoint;

    private int _currentWaveIndex = 0;
    private int _currentEnemyTypeIndex = 0;
    private int _spawnedCountForType = 0;
    private float _spawnTimer = 0f;
    private float _waveDelayTimer = 0f;
    private bool _waveInProgress = false;
    private bool _spawningWaves = false;

    #endregion

}

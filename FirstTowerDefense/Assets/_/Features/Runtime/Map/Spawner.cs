using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    #region Api Unity

    private void Update()
    {
        if (_enemyCount >= _enemiesPerWave.Length) return;
        _nextSpawn += Time.deltaTime;
        if (_waveInProgress)
        {
            if (_spawnThisWave < _enemiesPerWave[_enemyCount] && _nextSpawn >= _spawnRate)
            {
                SpawnEnemy();
                _spawnThisWave++;
                _nextSpawn = 0f;
            }
        }

        if (_spawnThisWave >= _enemiesPerWave[_enemyCount])
        {
            _waveInProgress = false;
            _nextSpawn = 0f;
        }
        else
        {
            if (_nextSpawn >= _spawnRate)
            {
                _enemyCount++;
                _spawnThisWave = 0;
                _waveInProgress = true;
                _nextSpawn = 0f;
            }
        }

    }

    #endregion
    
    
    #region Utils

    private void SpawnEnemy()
    {
        int enemy = Random.Range(0, _enemyPrefab.Length);
        Instantiate(_enemyPrefab[enemy], transform.position, Quaternion.identity);
    }
    
    #endregion
    
    
    #region Private And Protected
    
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private float _waveDelay = 5f;
    [SerializeField] private int[] _enemiesPerWave = {5,8,12};
    
    private float _nextSpawn;
    private Vector3 _spawnPosition;
    private int _enemyCount;
    private int _spawnThisWave;
    private bool _waveInProgress;

    #endregion

}

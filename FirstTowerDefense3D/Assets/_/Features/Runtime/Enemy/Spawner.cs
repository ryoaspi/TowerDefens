using TheFundation.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    public class Spawner : FBehaviour
    {
        #region Publics

        public int m_waveIndex;
        public float m_spawnInterval = 3f;
        
        #endregion
        
        
        #region Api Unity

        protected override void Start()
        {
            base.Start();
            InitializeSpawner();
        }

        private void Update()
        {
            UpdateSpawning( Time.deltaTime );
        }

        #endregion
        
        
        #region Utils

        /// <summary>
        /// Lance la prochaine vague d’ennemis depuis ce spawner.
        /// </summary>
        public void TriggerNextWave()
        {
            m_waveIndex++;
            _timerSinceLastSpawn = 0f;
        }
        
        #endregion
        
        #region Main Methods

        private void InitializeSpawner()
        {
            _timerSinceLastSpawn = 0f;
            _currentEnemyCount = 0;
        }

        private void UpdateSpawning(float deltaTime)
        {
            if ( _currentEnemyCount >= _enemyPerWave )
                return;
            
            _timerSinceLastSpawn += deltaTime;

            if (_timerSinceLastSpawn >= m_spawnInterval)
            {
                SpawnEnemy();
                _timerSinceLastSpawn = 0f;
            }
        }

        private void SpawnEnemy()
        {
            var enemy = Instantiate( _enemyPrefab, transform.position, Quaternion.identity );
            // enemy.Initialize(_path); // Exemple d'initialisation avec chemin
            _currentEnemyCount++;
        }
        
        #endregion
        
        
        #region Private and Protected

        [SerializeField] private GameObject _enemyPrefab;
        // [SerializeField] private Path _path; //Référence vers la route que doivent suivre les ennemis
        [SerializeField] private int _enemyPerWave = 5;

        private int _currentEnemyCount;
        private float _timerSinceLastSpawn;

        #endregion
    }
}

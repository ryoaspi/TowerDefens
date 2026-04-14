using TheFundation.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    public class Enemy : FBehaviour
    {


        #region Publics

        public EnemyDefinition m_definition => _definition;
        public EnemyRuntimeStats m_runtimeStats => _runtimeStats;



        #endregion
        
        
        #region Api Unity

        private void Awake()
        {
            _movement = GetComponent< EnemyMovement >();
        }

        #endregion


        #region Utils

        public void Initialize( EnemyDefinition definition , EnemyRuntimeStats runtimeStats , EnemyPath path)
        {
            if (!definition)
            {
                Error(" [ Enemy ] EnemyDefinition is null", this);
                return;
            }
            
            _definition = definition;
            _runtimeStats = runtimeStats;

            InitializeVisual();
            
            if ( _movement )
                _movement.Initialize( this, path );
        }

        #endregion
        
        
        #region Main Methods

        private void InitializeVisual()
        {
            if ( !_visualRoot )
                return;
            
            if ( _currentVisualInstance )
                Destroy( _currentVisualInstance );
            
            if ( !_definition.m_visualPrefab )
                return;
            
            _currentVisualInstance = Instantiate( _definition.m_visualPrefab , _visualRoot.position 
                , _visualRoot.rotation , _visualRoot );
            
        }
        
        #endregion
        
        
        #region Private and protected
        
        [SerializeField] private Transform _visualRoot;
        
        private EnemyDefinition _definition;
        private EnemyRuntimeStats _runtimeStats;
        private EnemyMovement _movement;
        private GameObject _currentVisualInstance;

        #endregion
    }
}

using TheFundation.Runtime;
using UnityEngine;

namespace Enemy.Runtime
{
    public class EnemyMovement : FBehaviour
    {
        #region Api Unity

        private void Update()
        {
            if ( !_isInitialized )
                return;

            UpdateMovement( Time.deltaTime );
        }

        #endregion
        
        
        #region Utils

        public void Initialize( Enemy enemy , EnemyPath path )
        {
            if (!enemy)
            {
                Error( " [ EnemyMovement ] Enemy is null" , this );
                return;
            }

            _enemy = enemy;
            _path = path;

            _currentWaypointIndex = 0;
            _targetWaypoint = _path.GetWaypoint(0);
            
            _isInitialized = true;
            
            
        }
        
        #endregion
        
        
        #region Main Methods

        private void UpdateMovement( float deltaTime )
        {
            if ( !_targetWaypoint )
                return;
            
            Vector3 direction = _targetWaypoint.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation( direction );
                transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, _rotationSpeed * deltaTime );
            }

            float speed = _enemy.m_runtimeStats.m_moveSpeed;

            transform.position = Vector3.MoveTowards( transform.position, _targetWaypoint.position, speed * deltaTime );

            float distance = Vector3.Distance( transform.position, _targetWaypoint.position );

            if ( distance < _waypointThreshold ) GoToNextWaypoint();
        }

        private void GoToNextWaypoint()
        {
            _currentWaypointIndex++;

            if ( _currentWaypointIndex >= _path.m_waypointCount )
            {
                ReachDestination();
                return;
            }

            _targetWaypoint = _path.GetWaypoint(_currentWaypointIndex);
        }

        private void ReachDestination()
        {
            Destroy( gameObject );
        }
        
        #endregion
        
        
        #region Private and Protected

        [SerializeField] private float _waypointThreshold = 0.1f;
        [SerializeField] private float _rotationSpeed = 10f;

        private Enemy _enemy;
        private EnemyPath _path;
        
        private Transform _targetWaypoint;
        private int _currentWaypointIndex;
        
        
        private bool _isInitialized;

        #endregion
    }
}

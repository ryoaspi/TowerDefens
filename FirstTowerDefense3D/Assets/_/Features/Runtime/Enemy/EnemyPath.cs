using UnityEngine;

namespace Enemy.Runtime
{
    public class EnemyPath : MonoBehaviour
    {
        #region Publics

        public int m_waypointCount => _waypoints.Length;

        #endregion
        
        
        #region Utils

        public Transform GetWaypoint(int index)
        {
            if ( index < 0 || index >= _waypoints.Length)
                return null;
            
            return _waypoints[index];
        }
        
        #endregion
        
        
        #region Privates and Protected
        
        [SerializeField] private Transform[] _waypoints;
        
        #endregion
    }
}

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Publics
    
    
    
    #endregion
    
    
    #region Api

    private void Start()
    {
        _manager = FindAnyObjectByType<WaypointsManager>();
    }

    private void Update()
    {
        
        Transform target = _manager.m_waypoints[_waypointIndex];
        Vector3 direction = target.position - transform.position; 
        transform.Translate(direction.normalized * (Time.deltaTime * _speed), Space.World);
        
        if (Vector3.Distance(transform.position, _manager.m_waypoints[_waypointIndex].position) < 0.1f)
        {
            _waypointIndex++;
        }

        if (_waypointIndex == _manager.m_waypoints.Length)
        {
            gameObject.SetActive(false);
        }
    }

    #endregion
    
    
    #region Main Methods
    
    
    
    #endregion
    
    
    #region Utils
    
    
    
    #endregion
    
    
    #region Private And Methods

    private Transform[] _waypoints;
    [SerializeField] private float _speed;
    private int _waypointIndex;
    private WaypointsManager _manager;


    #endregion
}

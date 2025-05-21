using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Publics
    
    
    
    #endregion
    
    
    #region Api

    private void Start()
    {
        WaypointsManager manager = FindAnyObjectByType<WaypointsManager>();
        if (manager != null)
        {
            _waypoints = manager.m_waypoints;
            Debug.Log("Waypoints reçus : " + _waypoints.Length);
        }
        else Debug.LogWarning("WaypointsManager is null");
        
        _gameObject= gameObject;
    }

    private void Update()
    {
        if (_waypoints == null || _waypointIndex >= _waypoints.Length) return;
        Transform target = _waypoints[_waypointIndex];
        Vector3 direction = target.position - transform.position; 
        transform.Translate(direction.normalized * (Time.deltaTime * _speed), Space.World);
        
        if (Vector3.Distance(transform.position, _waypoints[_waypointIndex].position) < 0.1f)
        {
            _waypointIndex++;
        }

        if (_waypointIndex == _waypoints.Length)
        {
            _gameObject.SetActive(false);
        }
    }

    #endregion
    
    
    #region Main Methods
    
    
    
    #endregion
    
    
    #region Utils
    
    
    
    #endregion
    
    
    #region Private And Methods

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    private int _waypointIndex;
    private GameObject _gameObject;
    

    #endregion
}

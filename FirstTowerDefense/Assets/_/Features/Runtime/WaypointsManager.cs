using System.Linq;
using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    #region Public
    
    public Transform[] m_waypoints => _waypoints; // tableau contenant tout les points du chemin
    
    #endregion
    
    #region Api Unity
    void Start()
    {
        GenerateWaypointsPath();
    }

    
    void Update()
    {
        
    }
    
    #endregion
    
    #region Main Methods

  
    private void GenerateWaypointsPath()
    {
        Cells[] allCells = FindObjectsOfType<Cells>(); // récupere toutes les cellules de type path
        Debug.Log("Nombre de cellules trouvées : " + allCells.Length);
        
        // Garde uniquement les cellules  de type path
        var pathCells = allCells
            .Where(cells => cells.m_cellTypeEffect == Cells.CellType.Path)
            .OrderBy(cells => cells.m_pathIndex) // trier par ordre croissant
            .ToArray();
        Debug.Log("Nombre de cellules PATH trouvées : " + pathCells.Length);
        
        // stocke tous leur transform dans le tableau
        _waypoints = new Transform[pathCells.Length];
        for (int i = 0; i < pathCells.Length; i++)
        {
            _waypoints[i] = pathCells[i].transform; 
        }

        for (int i = 0; i < _waypoints.Length; i++)
        {
            Debug.Log($"Waypoint {i} : {_waypoints[i].name} at {_waypoints[i].position}");
        }
    }
    
    #endregion
    
    #region Private And Protected
    
    private  Transform[] _waypoints;
    #endregion
}

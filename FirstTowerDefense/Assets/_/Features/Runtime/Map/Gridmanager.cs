using UnityEngine;

public class Gridmanager : MonoBehaviour
{
   #region Api Unity
   
    private void Awake()
    {
        GenerateGrid();
    }
    
    #endregion
    
    
    #region Utils
    
    [ContextMenu("Generate Grid")]
    private void GenerateGrid()
    {
        var size = _gridSize.x * _gridSize.y;
        GameObject currentcell = null;
        Vector3Int currentcellpos = new Vector3Int();
        for (int i = 0; i < size; i++)
        {
            Vector2Int position = Get2DCoordinates(i,_gridSize);
            currentcellpos = new Vector3Int(position.x,position.y,0);
            currentcell = Instantiate(_grid,currentcellpos,Quaternion.identity, _parentContainer );
            currentcell.name = "Cell : " + i;
        }
    }

    private Vector2Int Get2DCoordinates(int i, Vector2Int map)
    {
        int valOne = i % map.y;
        int valTwo = i / map.y;
        return new Vector2Int(valOne, valTwo);
    }
    
    #endregion
    
    
    #region Private And Protected
    
    [SerializeField] private GameObject _grid;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Transform _parentContainer;
    
    private GameObject[] _grids;
    
    #endregion
}

using UnityEngine;
using UnityEngine.Serialization;



public class Cells : MonoBehaviour
{
    #region Publics

    public enum CellType{Path, Buildable};
    public int m_pathIndex => _pathIndex;
    public CellType m_cellTypeEffect => _cellTypeEffect;
    
    #endregion
    
    
    #region Api Unity

    private void Start()
    {
        _colors = GetComponent<SpriteRenderer>();
        CellTypeSetting();
        
    }

    private void OnMouseDown()
    {
        if (_isOccupied == false)
        {
            Instantiate(_tourPrefab,transform.position,Quaternion.identity);
            _isOccupied = true;
        }
        
    }

    #endregion
    
    
    #region Main Methods
    
    public void CellTypeSetting()
    {
        switch (m_cellTypeEffect)
        {
            
            case CellType.Path:
                _colors.color = Color.green ;
                _isOccupied = true;
                break;
            case CellType.Buildable:
                _colors.color = Color.gray;
                _isOccupied = false;
                break;
        }
    }
    
    #endregion
    
    
    #region Utils


    
    #endregion
    

    #region Private And Protected

    [SerializeField] private GameObject _tourPrefab;
    [SerializeField] private int _pathIndex;
    [SerializeField] private CellType _cellTypeEffect;
    private bool _isOccupied = false;
    private SpriteRenderer _colors;

    #endregion
}

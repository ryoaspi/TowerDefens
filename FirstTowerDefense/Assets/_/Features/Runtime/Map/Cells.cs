using UnityEngine;

public class Cells : MonoBehaviour
{
    #region Publics

    public enum CellType{Path, Buildable, Empty, Mine, Spawn, Base};
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
        if (_isOccupied == false && _cellTypeEffect == CellType.Buildable)
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
            case CellType.Empty:
                gameObject.SetActive(false);
                break;
            case CellType.Mine:
                _colors.color = Color.yellow;
                _isOccupied = false;
                break;
            case CellType.Spawn:
                _colors.color = Color.red;
                _isOccupied = true;
                GetComponent<Spawner>().enabled = true;
                break;
            case CellType.Base:
                _colors.color = Color.blue;
                _isOccupied = true;
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
    private bool _isOccupied;
    private SpriteRenderer _colors;

    #endregion
}

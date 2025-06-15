using UnityEngine;
using UnityEngine.UI;

public class BuildMenuUI : MonoBehaviour
{
    #region Publics
    
    public static BuildMenuUI m_instance;
    
    #endregion
    
    
    #region Api Unity

    private void Awake()
    {
        if (m_instance == null) m_instance = this;
        else Destroy(gameObject);
        
        _buildMenuPanel.SetActive(false);
        _closeButton.onClick.AddListener(CloseMenu);
        _pinButton.onClick.AddListener(TogglePin);
    }

    #endregion
    
    
    #region Main Methods

    public void OpenMenu(CellInteractor cell, Vector3 worldPosition)
    {
        if (_isPinned) return; // Ne rien faire si épinglé
        
        _currentCell = cell;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        _buildMenuPanel.transform.position = screenPos;
        _buildMenuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        if (_isPinned) return; // Ne pas fermer si épinglé
        _buildMenuPanel.SetActive(false);
        _currentCell = null;
    }

    public void ForceCloseMenu()
    {
        _buildMenuPanel.SetActive(false);
        _currentCell = null;
        _isPinned = false;
        UpdatePinIcon();
    }

    public void OnBuildTowerButton()
    {
        if (_currentCell != null)
        {
            _currentCell.BuildTower();
        }
        CloseMenu();
    }

    private void TogglePin()
    {
        _isPinned = !_isPinned;
        UpdatePinIcon();
    }

    private void UpdatePinIcon()
    {
        // changer l'icône du bouton selon l'état 
        // _pinButton.GetComponentInChildren<Text>().text = _isPinned ? "Unpin" : "Pin";
    }

    #endregion
    
    
    #region Private And Protected

    [SerializeField] private GameObject _buildMenuPanel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _pinButton;

    private bool _isPinned = false;
    private CellInteractor _currentCell;

    #endregion
}
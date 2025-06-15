using UnityEngine;
using UnityEngine.EventSystems;


public class CellInteractor : MonoBehaviour
{
    #region Publics

    public bool m_isBuildable = true;
    
    #endregion
    
    
    #region Api Unity

    private void OnMouseDown()
    {
        if (!m_isBuildable) return;

        // Si clic sur UI, on ignore
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        BuildMenuUI.m_instance.OpenMenu(this, transform.position);
        
    }

    #endregion


    #region Main Method

    public void BuildTower()
    {
        if (m_isOccupied) return;

        Instantiate(_towerPrefab, transform.position, Quaternion.identity);
        m_isOccupied = true;
    }

    #endregion


    #region  Private And Protected
    
    [SerializeField] private GameObject _towerPrefab; // Le prefab de la tourelle
    private bool m_isOccupied = false; // Suivi de l'état de la cellule

    #endregion
}

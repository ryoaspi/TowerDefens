using TheFundation.Runtime;
using UnityEngine;

namespace Map.Runtime
{
    public class Cell : FBehaviour
    {
        #region Publics

        /// <summary>
        /// Définit les types possibles pour une cellule.
        /// </summary>
        public enum CellType
        {
            Path,
            Buildable,
            Empty,
            Mine,
            Spawn,
            Base
        }

        /// <summary>
        /// Index du chemin, utilisé pour ordonner le chemin des ennemis.
        /// </summary>
        public int m_pathIndex => _pathIndex;

        /// <summary>
        /// Expose le type de la cellule à l’extérieur.
        /// </summary>
        public CellType m_cellTypeEffect => _cellTypeEffect;

        public bool m_isOccupied => _isOccupied;

        #endregion

        #region API Unity (Awake, Start, Update, etc.)

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _ApplyCellState();
        }

        #endregion

        #region Utils (méthodes publics)

        public void Initialize(CellType cellType, int pathIndex)
        {
            _cellTypeEffect = cellType;
            _pathIndex = pathIndex;

            _ApplyCellState();
        }

        public void SetOccupied(bool isOccupied)
        {
            _isOccupied = isOccupied;
        }

        public bool CanBuild()
        {
            return _cellTypeEffect == CellType.Buildable && !_isOccupied;
        }

        #endregion

        #region Main Methods (méthodes private)

        private void _ApplyCellState()
        {
            if (_renderer == null)
            {
                Warning("[Cell] Renderer is missing on cell prefab.", this);
                return;
            }

            switch (_cellTypeEffect)
            {
                case CellType.Path:
                    _renderer.material.color = Color.green;
                    _isOccupied = true;
                    break;

                case CellType.Buildable:
                    _renderer.material.color = Color.gray;
                    _isOccupied = false;
                    _EnsureCollider();
                    break;

                case CellType.Empty:
                    gameObject.SetActive(false);
                    break;

                case CellType.Mine:
                    _renderer.material.color = Color.yellow;
                    _isOccupied = false;
                    _EnsureCollider();
                    break;

                case CellType.Spawn:
                    _renderer.material.color = Color.red;
                    _isOccupied = true;
                    break;

                case CellType.Base:
                    _renderer.material.color = Color.blue;
                    _isOccupied = true;
                    break;
            }

            _ApplyVisual();
        }

        private void _EnsureCollider()
        {
            if (GetComponent<Collider>() == null)
                gameObject.AddComponent<BoxCollider>();
        }

        private void _ApplyVisual()
        {
            if (_cellTypeEffect == CellType.Path)
                transform.localScale = Vector3.one;
            else
                transform.localScale = Vector3.one * _visualScale;
        }

        #endregion

        #region Private and Protected

        [Header("Paramètres de cellule")]
        [SerializeField] private int _pathIndex;
        [SerializeField] private CellType _cellTypeEffect;
        [SerializeField] private float _visualScale = 0.9f;

        private bool _isOccupied;
        private Renderer _renderer;

        #endregion
    }
}
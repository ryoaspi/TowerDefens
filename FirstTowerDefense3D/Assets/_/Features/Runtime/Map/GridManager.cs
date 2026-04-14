using System;
using TheFundation.Runtime;
using UnityEngine;

namespace Map.Runtime
{
    public class GridManager : FBehaviour
    {
        #region Publics

        #endregion

        #region API Unity (Awake, Start, Update, etc.)

        private void Awake()
        {
            if (_generateOnAwake)
                GenerateGrid();
        }

        #endregion

        #region Utils (méthodes publics)

        [ContextMenu("Generate Grid")]
        private void GenerateGrid()
        {
            if (!ValidationGeneration())
                return;

            ClearGrid();

            string[] rows = _mapDefinition.m_layoutText.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries);

            int pathIndex = 0;

            for (int y = 0; y < rows.Length; y++)
            {
                string[] columns = rows[y].Split(',');

                for (int x = 0; x < columns.Length; x++)
                {
                    Cell.CellType cellType = ParseCellType(columns[x].Trim());

                    Vector3 position = new Vector3(
                        x * _mapDefinition.m_cellSize,
                        0f,
                        -y * _mapDefinition.m_cellSize);

                    GameObject cellObject = Instantiate(
                        _cellPrefab,
                        position,
                        Quaternion.identity,
                        _parentContainer);

                    Cell cell = cellObject.GetComponent<Cell>();

                    if (cell == null)
                    {
                        Error("[GridManager] Generated object has no Cell component.", cellObject);
                        continue;
                    }

                    int currentPathIndex = cellType == Cell.CellType.Path ? pathIndex++ : -1;
                    cell.Initialize(cellType, currentPathIndex);
                }
            }
        }

        [ContextMenu("Clear Grid")]
        public void ClearGrid()
        {
            if (!_parentContainer)
                return;

            for (int i = _parentContainer.childCount - 1; i >= 0; i--)
            {
                Transform child = _parentContainer.GetChild(i);

                if (Application.isPlaying)
                    Destroy(child.gameObject);
                else
                    DestroyImmediate(child.gameObject);
            }
        }

        public Vector3 GetWorldPosition(Vector2Int gridPosition)
        {
            float step = _cellSize + _gridSpacing;

            float x = gridPosition.x * step;
            float z = gridPosition.y * step;

            return new Vector3(x, 0f, z);
        }

        #endregion

        #region Main Methods (méthodes private)

        private bool ValidationGeneration()
        {
            if (!_mapDefinition)
            {
                Error("[GridManager] Map definition is missing.", this);
                return false;
            }

            if (!_cellPrefab)
            {
                Error("[GridManager] Cell prefab is missing.", this);
                return false;
            }

            if (!_parentContainer)
            {
                Error("[GridManager] Parent container is missing.", this);
                return false;
            }

            if (!_cellPrefab.GetComponent<Cell>())
            {
                Error("[GridManager] Cell prefab does not contain a Cell component.", _cellPrefab);
                return false;
            }

            if (string.IsNullOrWhiteSpace(_mapDefinition.m_layoutText))
            {
                Error("[GridManager] Layout text is empty.", this);
                return false;
            }

            return true;
        }

        private Cell.CellType ParseCellType(string cellCode)
        {
            switch (cellCode)
            {
                case "P":
                    return Cell.CellType.Path;

                case "Bu":
                    return Cell.CellType.Buildable;

                case "E":
                    return Cell.CellType.Empty;

                case "M":
                    return Cell.CellType.Mine;

                case "S":
                    return Cell.CellType.Spawn;

                case "Ba":
                    return Cell.CellType.Base;

                default:
                    Warning($"[GridManager] Unknown cell code : {cellCode}. Defaulting to Empty.", this);
                    return Cell.CellType.Empty;
            }
        }

        #endregion

        #region Private and Protected

        [Header("Map Definition")]
        [SerializeField] private MapDefinition _mapDefinition;

        [Header("Grid Setup")]
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private Transform _parentContainer;
        [SerializeField] private float _cellSize = 1f;
        [SerializeField] private float _gridSpacing = 0f;
        [SerializeField] private bool _generateOnAwake = true;

        #endregion
    }
}
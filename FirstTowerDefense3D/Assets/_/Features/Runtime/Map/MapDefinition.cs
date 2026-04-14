using System;
using UnityEngine;

namespace Map.Runtime
{
    [CreateAssetMenu(fileName = "MapDefinition", menuName = "Map/Map Definition")]
    public class MapDefinition : ScriptableObject
    {
        #region Publics

        [TextArea(5, 20)] 
        public string m_layoutText;

        public float m_cellSize = 1f;

        #endregion
    }
}

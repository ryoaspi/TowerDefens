using UnityEngine;

namespace Tower.Runtime
{
    [CreateAssetMenu(fileName = "BuildDefinition", menuName = "TowerDefense/Build/Build Definition")]
    public class BuildDefinition : ScriptableObject
    {
        #region Publics

        public string m_buildId;
        public BuildType m_buildType;
        public GameObject m_prefab;

        #endregion
    }

    public enum BuildType
    {
        Tower,
        Mine,
        Utility
    }
}

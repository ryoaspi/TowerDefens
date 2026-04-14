using UnityEngine;

namespace Enemy.Runtime
{
    [CreateAssetMenu(fileName = "EnemyDefinition", menuName = "Enemy/Enemy Definition")]
    public class EnemyDefinition : ScriptableObject
    {
        #region Publics

        public string m_enemyId;
        public string m_displayName;
        public float m_maxHealth = 10f;
        public float m_moveSpeed = 2f;
        public int m_softCurrencyReward = 1;
        public int m_permanentCurrencyReward = 0;
        public int m_baseDamage = 1;
        public GameObject m_visualPrefab;
        
        #endregion
    }
}

using System;
using UnityEngine;

namespace Enemy.Runtime
{
    [Serializable]
    public class EnemyRuntimeStats
    {
        #region Publics
        
        public float m_maxHealth;
        public float m_currentHealth;
        public float m_moveSpeed;
        public int m_softCurrencyReward;
        public int m_permanentCurrencyReward;
        public int m_baseDamage;

        public void CopyFromDefinition(EnemyDefinition definition)
        {
            if( !definition )
                return;
            
            m_maxHealth = definition.m_maxHealth;
            m_currentHealth = definition.m_maxHealth;
            m_moveSpeed = definition.m_moveSpeed;
            m_softCurrencyReward = definition.m_softCurrencyReward;
            m_permanentCurrencyReward = definition.m_permanentCurrencyReward;
            m_baseDamage = definition.m_baseDamage;
        }

        #endregion
        
        
        #region Utils

        public void ApplyMultiplier(float multiplier)
        {
            if ( multiplier <= 0f )
                return;
            
            m_maxHealth  *= multiplier;
            m_currentHealth = m_maxHealth;
            m_moveSpeed  *= multiplier;
            m_softCurrencyReward = Mathf.RoundToInt(m_softCurrencyReward * multiplier);
            m_permanentCurrencyReward =  Mathf.RoundToInt(m_permanentCurrencyReward * multiplier);
            m_baseDamage   = Mathf.RoundToInt(m_baseDamage * multiplier);
        }
        
        #endregion
    }
}

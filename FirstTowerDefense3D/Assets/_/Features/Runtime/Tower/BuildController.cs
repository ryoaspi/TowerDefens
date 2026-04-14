using System;
using Map.Runtime;
using TheFundation.Runtime;
using UnityEngine;

namespace Tower.Runtime
{
    public class BuildController : FBehaviour
    {
        #region Publics
        
        public BuildDefinition m_currentBuildDefinition;
        
        #endregion
        
        
        #region Api Unity

        private void Awake()
        {
            if (!m_currentBuildDefinition)
                Warning("[BuildController] Current build definition is null");
        }

        #endregion
        
        
        #region Utils

        public bool TryBuild(Cell targetCell)
        {
            if (!CanBuild(targetCell))
                return false;

            GameObject towerInstance = Instantiate(m_currentBuildDefinition.m_prefab,
                targetCell.transform.position,
                Quaternion.identity);

            targetCell.SetOccupied(true);
            Info($"[BuildController] Tower build on cell : {targetCell.name}", towerInstance);
            
            return true;
        }
        
        #endregion
        
        
        #region Main Methods

        private bool CanBuild(Cell targetCell)
        {
            if (!targetCell)
                return false;

            if (!m_currentBuildDefinition)
                return false;

            if (!m_currentBuildDefinition.m_prefab)
                return false;

            if (!targetCell.CanBuild())
                return false;

            return true;
        }
        
        #endregion
        
    }
}

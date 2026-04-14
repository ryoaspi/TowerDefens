using Map.Runtime;
using TheFundation.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tower.Runtime
{
    public class BuildInputAdapter : FBehaviour
    {
        #region Publics

        public Camera m_camera;
        public LayerMask m_cellLayer;
        public BuildController m_buildController;
        public InputActionReference m_clickAction;
        public InputActionReference m_pointerPositionAction;

        #endregion

        #region API Unity (Awake, Start, Update, etc.)

        private void OnEnable()
        {
            if (m_clickAction != null)
                m_clickAction.action.Enable();

            if (m_pointerPositionAction != null)
                m_pointerPositionAction.action.Enable();
        }

        private void OnDisable()
        {
            if (m_clickAction != null)
                m_clickAction.action.Disable();

            if (m_pointerPositionAction != null)
                m_pointerPositionAction.action.Disable();
        }

        private void Update()
        {
            if (!m_clickAction || !m_pointerPositionAction)
                return;

            if (m_clickAction.action.WasPressedThisFrame())
                _HandleClick();
        }

        #endregion

        #region Utils (méthodes publics)

        #endregion

        #region Main Methods (méthodes private)

        private void _HandleClick()
        {
            if (!m_camera || !m_buildController)
                return;

            Vector2 pointerPosition = m_pointerPositionAction.action.ReadValue<Vector2>();
            Ray ray = m_camera.ScreenPointToRay(pointerPosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, 100f, m_cellLayer))
                return;

            Cell cell = hit.collider.GetComponent<Cell>();

            if (!cell)
                return;

            m_buildController.TryBuild(cell);
        }

        #endregion

        #region Private and Protected

        #endregion
    }
}
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GridInput : MonoBehaviour
    {
        Camera cam;

        #region UNITY_METHODS
        private void Start()
        {
            cam = Camera.main;
        }
        private void Update()
        {
            MouseInput();
        }
        #endregion

        #region METHODS
        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0) && !MouseUtils.MouseIsOnUI())
            {
                CheckCell();
            }
        }
        private void CheckCell()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                if (rayHit.collider == null)
                    return;
                EventManager.TriggerEvent(Events.PLACE_INTO_CELL, true, new object[] { rayHit.collider.gameObject });
            }
        }
        #endregion
        #region ACTIONS
        #endregion
    }
}
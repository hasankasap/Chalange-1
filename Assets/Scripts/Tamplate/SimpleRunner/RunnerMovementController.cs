using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RunnerMovementController : MonoBehaviour
    {
        #region Properties
        protected float frontSpeed, sideSpeed, sensitivity, sideMovementClamp;
        protected float sideInput;
        protected Vector3 movementDirection;
        #endregion

        protected bool canMove = false;
        #region UNITY_METHODS
        protected virtual void Update()
        {
            if (canMove) 
            {
                Move();
            }
        }
        #endregion

        #region METHODS
        protected virtual void Move()
        {
            FrontMovement();
            SideMovement();
        }
        protected virtual void FrontMovement()
        {
            transform.position += movementDirection * frontSpeed * Time.deltaTime;
        }
        protected virtual void SideMovement() 
        {
            Vector3 tempPos = transform.position;
            tempPos += transform.right * sideSpeed * SideMovementInput();
            tempPos.x = Mathf.Clamp(tempPos.x, -sideMovementClamp, sideMovementClamp);
            transform.position = tempPos;
        }
        protected virtual float SideMovementInput()
        {
            return sideInput * sensitivity;
        }
        public virtual void SetSideMovementInput(float input)
        {
            sideInput = input;
        }
        #endregion
        #region ACTIONS
        #endregion
    }
}
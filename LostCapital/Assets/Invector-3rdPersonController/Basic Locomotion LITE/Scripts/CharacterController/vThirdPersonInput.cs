﻿using System;
using UnityEngine;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace Invector.CharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region variables

        [Header("Default Inputs")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;
        public KeyCode crouchInput = KeyCode.C;
        public KeyCode flashPoint = KeyCode.F;
        public KeyCode interactive = KeyCode.E;
        public KeyCode changeFile = KeyCode.Q;
        public KeyCode openSmallMap = KeyCode.M;
        public KeyCode openMap = KeyCode.N;
        public KeyCode Mouse = KeyCode.P;
        public KeyCode throwaway = KeyCode.Z;


        [Header("Camera Settings")]
        public string rotateCameraXInput ="Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        protected vThirdPersonCamera tpCamera;                // acess camera info        
        [HideInInspector]
        public string customCameraState;                    // generic string to change the CameraState        
        [HideInInspector]
        public string customlookAtPoint;                    // generic string to change the CameraPoint of the Fixed Point Mode        
        [HideInInspector]
        public bool changeCameraState;                      // generic bool to change the CameraState        
        [HideInInspector]
        public bool smoothCameraState;                      // generic bool to know if the state will change with or without lerp  
        [HideInInspector]
        public bool keepDirection;                          // keep the current direction in case you change the cameraState

        public Light Flash;

        protected vThirdPersonController cc;                // access the ThirdPersonController component                

        #endregion

        protected virtual void Start()
        {
            CharacterInit();
        }

        protected virtual void CharacterInit()
        {
            cc = GetComponent<vThirdPersonController>();
            if (cc != null)
                cc.Init();

            tpCamera = FindObjectOfType<vThirdPersonCamera>();
            if (tpCamera) tpCamera.SetMainTarget(this.transform);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected virtual void LateUpdate()
        {
            if (cc == null) return;             // returns if didn't find the controller		    
            InputHandle();                      // update input methods
            UpdateCameraStates();               // update camera states
        }

        protected virtual void FixedUpdate()
        {
            cc.AirControl();
            CameraInput();
        }

        protected virtual void Update()
        {
            cc.UpdateMotor();                   // call ThirdPersonMotor methods               
            cc.UpdateAnimator();                // call ThirdPersonAnimator methods		               
        }

        protected virtual void InputHandle()
        {
            ExitGameInput();
            CameraInput();

            if (!cc.lockMovement)
            {
                MoveCharacter();
                SprintInput();
                StrafeInput();
                JumpInput();
                CrouchInput();
                FlashUP_and_Down();
                Throw();
                Interactive();
                changeItem();
            }
        }

       

        #region Basic Locomotion Inputs      

        protected virtual void MoveCharacter()
        {            
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.y = Input.GetAxis(verticallInput);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput) && !cc.isDead)
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput) && !cc.isDead)
                cc.Sprint(true);
            else if(Input.GetKeyUp(sprintInput) && !cc.isDead)
                cc.Sprint(false);
        }

        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) &&!cc.isDead)
                cc.Jump();
        }

        protected virtual void Interactive()
        {
            if (Input.GetKeyDown(interactive) && !cc.isDead)
                cc.InteractiveItem();
        }

        protected virtual void ExitGameInput()
        {
            // just a example to quit the application 
            if (Input.GetKeyDown(KeyCode.Escape) && !cc.isDead)
            {
                if (!Cursor.visible)
                    Cursor.visible = true;
                else
                    Application.Quit();
            }
        }

        protected virtual void CrouchInput()
        {
            if(Input.GetKeyDown(crouchInput) && !cc.isDead)
                cc.Crouch();
        }

        protected virtual void FlashUP_and_Down()
        {
            if (Input.GetKeyDown(flashPoint) && Flash.enabled == true && !cc.isDead)
            {
                Flash.enabled = false;
            }
            else if(Input.GetKeyDown(flashPoint) && Flash.enabled == false && !cc.isDead)
            {
                Flash.enabled = true;
            }
        }

        protected virtual void Throw()
        {
            if (Input.GetKeyDown(throwaway) && !cc.isDead)
            {
                cc.ThrowItem();
            }
        }

        protected virtual void changeItem()
        {
            if (Input.GetKeyDown(changeFile) && !cc.isDead)
            {
                cc.changeitem();
            }
        }

        #endregion

        #region Camera Methods

        protected virtual void CameraInput()
        {
            if (tpCamera == null)
                return;
            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);

            // tranform Character direction from camera if not KeepDirection
            if (!keepDirection)
                cc.UpdateTargetDirection(tpCamera != null ? tpCamera.transform : null);
            // rotate the character with the camera while strafing        
            RotateWithCamera(tpCamera != null ? tpCamera.transform : null);            
        }

        protected virtual void UpdateCameraStates()
        {
            // CAMERA STATE - you can change the CameraState here, the bool means if you want lerp of not, make sure to use the same CameraState String that you named on TPCameraListData
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }            
        }

        protected virtual void RotateWithCamera(Transform cameraTransform)
        {
            if (cc.isStrafing && !cc.lockMovement && !cc.lockMovement)
            {                
                cc.RotateWithAnotherTransform(cameraTransform);                
            }
        }

        #endregion     
    }
}
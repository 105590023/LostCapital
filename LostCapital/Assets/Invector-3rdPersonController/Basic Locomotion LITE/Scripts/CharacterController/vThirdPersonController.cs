using UnityEngine;
using System.Collections;
using System;

namespace Invector.CharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        protected virtual void Start()
        {
#if !UNITY_EDITOR
                Cursor.visible = false;
#endif
        }

        public virtual void Sprint(bool value)
        {                                   
            isSprinting = value;            
        }

        public virtual void Strafe()
        {
            if (locomotionType == LocomotionType.OnlyFree) return;
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // conditions to do this action
            bool jumpConditions = isGrounded && !isJumping;
            // return if jumpCondigions is false
            if (!jumpConditions) return;
            // trigger jump behaviour
            jumpCounter = jumpTimer;            
            isJumping = true;
            // trigger jump animations            
            if (_rigidbody.velocity.magnitude < 1)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", 0.2f);
        }

        public virtual void RotateWithAnotherTransform(Transform referenceTransform)
        {
            var newRotation = new Vector3(transform.eulerAngles.x, referenceTransform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), strafeRotationSpeed * Time.fixedDeltaTime);
            targetRotation = transform.rotation;
        }

        public virtual void Crouch()
        {
            if (isCrouching) { isCrouching = false; }
            else if(!isCrouching) { isCrouching = true; }
        }

        public virtual void InteractiveItem()
        {
            if (RayHit.transform == null)
            {
                Debug.Log("NULL");
                useItem();
            }
            else if (RayHit.transform.tag == "Door_L" || RayHit.transform.tag == "Door_R")
            {
                Debug.Log("FBI Open UP");
                Debug.Log(RayHit.transform.name);
                
            }
            else
            {
                Debug.Log(RayHit.transform.name);
            }
        }

        public virtual void ThrowItem()
        { 
            if (String.IsNullOrEmpty(First_Prop))
            {
                Debug.Log("沒有東西啦幹!");
            }
            else if (String.IsNullOrEmpty(Second_Prop))
            {
                First_Prop = null;
                Debug.Log("身上得東西" + First_Prop + " " + Second_Prop);
            }
            else if (!(String.IsNullOrEmpty(Second_Prop)))
            {
                Debug.Log("抓到 亂丟垃圾");
                First_Prop = Second_Prop;
                Second_Prop = null;
                Debug.Log("身上得東西" + First_Prop +" "+ Second_Prop);
            }
        }

        public virtual void changeitem()
        {
            if (String.IsNullOrEmpty(First_Prop))
            {
                Debug.Log("沒有東西啦幹!");
            }
            else if (String.IsNullOrEmpty(Second_Prop))
            {
                Debug.Log("身上得東西 第一樣 : " + First_Prop + " 第二樣 : " + Second_Prop);
            }
            else if (!(String.IsNullOrEmpty(Second_Prop)))
            {
                Debug.Log("交換東西~");
                wait_Prop = First_Prop;
                First_Prop = Second_Prop;
                Second_Prop = wait_Prop;
                Debug.Log("身上得東西" + First_Prop + " " + Second_Prop);
            }
        }

    }
}
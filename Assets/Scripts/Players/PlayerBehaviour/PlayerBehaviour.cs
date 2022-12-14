using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.States;
using Interfaces;

namespace Players
{
    public enum PlayerStates
    {
        Idle = 0,
        Walk = 1,
        Run = 2,
        InAir=3,

    }

    public partial class PlayerBehaviour : StateMachineBase<PlayerBehaviour>
    {
        [field: SerializeField] PlayerData playerData;
        IInputEventProvider playerInput;
        Rigidbody playerRigidbody;
        Animator playerAnim;

        Vector3 relativeVelocity = new Vector3();

        int groundMask = 1 << 6;

        RaycastHit groundHit;
        RaycastHit prevGroundHit;
        RaycastHit frontHit;

        bool isSlope = false;

        [field: SerializeField] CameraData cameraData;

        [field: SerializeField] PlayerStates currentState = PlayerStates.Idle;


        void Start()
        {
            ChangeState(new PlayerBehaviour.Idle(this));
            playerInput = GetComponent<IInputEventProvider>();
            playerRigidbody = GetComponent<Rigidbody>();
            playerAnim = GetComponent<Animator>();
        }

        internal override void Update()
        {
            base.Update();
        }

        internal override void FixedUpdate()
        {
            base.FixedUpdate();
            IsGrounding();
            CheckFront();
        }

        private void IsGrounding()
        {
            if (Physics.SphereCast(transform.position + transform.up * 0.5f, 0.2f, -transform.up, out groundHit, 0.5f, groundMask))
            {
                if(groundHit.collider.TryGetComponent(out Rigidbody moveFloor))
                {
                    relativeVelocity = playerRigidbody.velocity - moveFloor.velocity;
                }
                else
                {
                    relativeVelocity = playerRigidbody.velocity;
                }

                //if (prevGroundHit.normal != groundHit.normal)
                //{
                //    playerRigidbody.velocity = Vector3.ProjectOnPlane(playerRigidbody.velocity, groundHit.normal);
                //}

                //prevGroundHit = groundHit;
            }
        }

        private void CheckFront()
        {
            //frontHit = Physics.SphereCastAll(transform.position + transform.up * 0.5f, 0.2f, -transform.up, 0.5f, groundMask);

            //for(int i = 0; i < frontHit.Length; i++)
            //{
            //    if (i == frontHit.Length - 1)
            //    {
            //        Debug.Log(frontHit[frontHit.Length - 1].point);
            //    }
            //}

            if (Physics.SphereCast(transform.position + transform.up * 2.0f + transform.forward * 0.5f, 0.2f, -transform.up, out frontHit, 3.0f, groundMask))
            {
                // それなりの段差
                if (2.0f - frontHit.distance > 1.2f)
                {
                    Debug.Log("それなりの段差");
                }
                // ちょっとした段差
                else if (2.0f - frontHit.distance > 0.5f)
                {
                    Debug.Log("ちょっとした段差");
                }
                // 無視できる段差
                else
                {
                    Debug.Log("無視できる段差");
                }
                //Debug.Log(2.0f - frontHit.distance);

                //// それなりの段差
                //if (frontHit.distance < 1.5f)
                //{
                //    Debug.Log("それなりの段差");
                //}
                //// ちょっとした段差
                //else if (frontHit.distance < 1.9f)
                //{
                //    Debug.Log("ちょっとした段差");
                //}
                //// 無視できる段差
                //else
                //{
                //    Debug.Log("無視できる段差");
                //}
                //Debug.Log(frontHit.distance);
            }
        }

        private void IsStep()
        {

        }

        private void ChangeDirection()
        {
            Vector3 inputDirection = cameraData.cameraTransform.rotation * new Vector3(playerInput.Move.x, 0, playerInput.Move.y);
            inputDirection.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputDirection), 30.0f * Time.deltaTime * playerInput.Move.magnitude);
        }

        private void PlayerGroundMove(float speed)
        {
            Vector3 applyForce = playerInput.Move.magnitude * transform.forward * speed - (new Vector3(relativeVelocity.x, 0, relativeVelocity.z));
            if (applyForce.magnitude > speed)
            {
                applyForce = applyForce.normalized * speed;
            }

            applyForce = Vector3.ProjectOnPlane(applyForce, groundHit.normal);

            playerRigidbody.AddForce(applyForce * Time.fixedDeltaTime * playerData.playerAccel, ForceMode.VelocityChange);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.up * 0.5f - transform.up * groundHit.distance, 0.2f);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + transform.up * 2.0f + transform.forward * 0.5f - transform.up * frontHit.distance, 0.2f);
        }
    }
}
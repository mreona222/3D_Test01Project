using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Utilities.States;

namespace Players
{
    public partial class PlayerBehaviour
    {
        private class Idle : StateBase<PlayerBehaviour>
        {
            public Idle(PlayerBehaviour _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.currentState = PlayerStates.Idle;
                machine.playerAnim.SetInteger("PlayerState", ((int)PlayerStates.Idle));
            }

            public override void OnUpdate()
            {
                if (machine.playerInput.Move == Vector2.zero)
                {

                }
                else
                {
                    machine.ChangeDirection();
                }
            }

            public override void OnFixedUpdate()
            {
                machine.PlayerGroundMove(machine.playerData.playerSpeed);

                if (machine.relativeVelocity.magnitude > 0.1f)
                {
                    machine.ChangeState(new PlayerBehaviour.Walk(machine));
                }

                if (!machine.groundHit.collider)
                {
                    machine.ChangeState(new PlayerBehaviour.InAir(machine));
                }
            }
        }
    }
}
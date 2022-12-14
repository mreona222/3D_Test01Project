using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.States;

namespace Players
{
    public partial class PlayerBehaviour
    {
        private class Run : StateBase<PlayerBehaviour>
        {
            public Run(PlayerBehaviour _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.currentState = PlayerStates.Run;
                machine.playerAnim.SetInteger("PlayerState", ((int)PlayerStates.Run));
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

                if (!machine.groundHit.collider)
                {
                    machine.ChangeState(new PlayerBehaviour.InAir(machine));
                }

                if (machine.relativeVelocity.magnitude < 4.0f)
                {
                    machine.ChangeState(new PlayerBehaviour.Walk(machine));
                }
            }
        }
    }
}
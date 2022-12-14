using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.States;

namespace Players
{
    public partial class PlayerBehaviour
    {
        private class InAir : StateBase<PlayerBehaviour>
        {
            public InAir(PlayerBehaviour _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.currentState = PlayerStates.InAir;
                machine.playerAnim.SetInteger("PlayerState", ((int)PlayerStates.InAir));
            }

            public override void OnUpdate()
            {

            }

            public override void OnFixedUpdate()
            {
                if (machine.groundHit.collider)
                {
                    machine.ChangeState(new PlayerBehaviour.Idle(machine));
                }
            }
        }
    }
}
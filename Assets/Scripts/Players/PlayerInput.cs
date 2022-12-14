using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Interfaces;

namespace Players
{
    public class PlayerInput : MonoBehaviour, IInputEventProvider
    {
        public Vector2 Move { get; set; }

        public bool Walk { get; set; }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }
    }
}
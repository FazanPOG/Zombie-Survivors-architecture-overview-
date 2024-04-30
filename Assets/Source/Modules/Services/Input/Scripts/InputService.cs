using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Modules.Input.Scripts
{
    public class InputService : IDisposable, IInputService
    {
        private PlayerInputAction _inputActions;
        private FloatingJoystick _floatingJoystick;

        public Vector2 MoveVector { get; private set; }

        public InputService(FloatingJoystick floatingJoystick) 
        {
            _floatingJoystick = floatingJoystick;

            _inputActions = new PlayerInputAction();

            _inputActions.Player.Enable();

            _inputActions.Player.Move.performed += MoveHandler;
            _inputActions.Player.Move.canceled += MoveHandler;

            _floatingJoystick.OnDragPerformed += JoystickHandler;
            _floatingJoystick.OnDragCanceled += JoystickHandler;
        }

        private void MoveHandler(InputAction.CallbackContext context)
        {
            if (_floatingJoystick.Performed == false)
            {
                if (context.performed)
                    MoveVector = context.ReadValue<Vector2>();

                if (context.canceled)
                    MoveVector = Vector2.zero;
            }
        }

        private void JoystickHandler() 
        {
            if (_floatingJoystick.Performed)
                MoveVector = _floatingJoystick.Direction;
            else
                MoveVector = Vector2.zero;
        }

        public void Dispose()
        {
            _inputActions.Player.Move.performed -= MoveHandler;
            _inputActions.Player.Move.canceled -= MoveHandler;

            _floatingJoystick.OnDragPerformed -= JoystickHandler;
            _floatingJoystick.OnDragCanceled -= JoystickHandler;
        }

    }
}

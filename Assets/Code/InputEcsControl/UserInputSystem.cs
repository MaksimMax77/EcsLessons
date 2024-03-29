using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.InputEcsControl
{
    public partial class UserInputSystem : SystemBase
    {
        private CharacterInput _characterInput;
        private float2 _moveInput;
        
        protected override void OnStartRunning()
        {
            _characterInput = new CharacterInput();
            _characterInput.Enable();
            _characterInput.Character.Move.started += OnMove;
            _characterInput.Character.Move.performed += OnMove;
            _characterInput.Character.Move.canceled += OnMove;
            
            _characterInput.Character.Fire.started += OnFireStarted;
            _characterInput.Character.Fire.canceled += OnFireCanceled;
            
            _characterInput.Character.Spurt.started += OnSpurtStarted;
            _characterInput.Character.Spurt.canceled += OnSpurtCanceled;
        }

        protected override void OnStopRunning()
        {
            _characterInput.Disable();
            _characterInput.Character.Move.started -= OnMove;
            _characterInput.Character.Move.performed -= OnMove;
            _characterInput.Character.Move.canceled -= OnMove;
            
            _characterInput.Character.Fire.started -= OnFireStarted;
            _characterInput.Character.Fire.canceled -= OnFireCanceled;
            
            _characterInput.Character.Spurt.started -= OnSpurtStarted;
            _characterInput.Character.Spurt.canceled -= OnSpurtCanceled;
        }
        
        protected override void OnUpdate()
        {
        }
        
        private void OnMove(InputAction.CallbackContext obj)
        {
            foreach (var inputData in SystemAPI.Query<RefRW<UserInputData>>())
            {
                inputData.ValueRW.inputMoveDirection = obj.ReadValue<Vector2>();
            }
        }

        private void OnFireStarted(InputAction.CallbackContext obj)
        {
            UpdateShootValue(1);
        }
        private void OnFireCanceled(InputAction.CallbackContext obj)
        {
            UpdateShootValue(0);
        }

        private void OnSpurtStarted(InputAction.CallbackContext obj)
        {
            UpdateSpurtValue(1);
        }
        
        private void OnSpurtCanceled(InputAction.CallbackContext obj)
        {
            UpdateSpurtValue(0);
        }
        
        private void UpdateShootValue(int value)
        {
            foreach (var inputData in SystemAPI.Query<RefRW<UserInputData>>())
            {
                inputData.ValueRW.shoot = value;
            }
        }
        
        private void UpdateSpurtValue(int value)
        {
            foreach (var inputData in SystemAPI.Query<RefRW<UserInputData>>())
            {
                inputData.ValueRW.spurt = value;
            }
        }
    }
}

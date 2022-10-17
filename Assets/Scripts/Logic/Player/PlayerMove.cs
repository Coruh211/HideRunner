﻿using Infrastructure.Services;
using StaticData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic.Player
{
    public class PlayerMove: MonoBehaviour
    {
        [SerializeField] private AnimatorController AnimatorController;
        
        private PlayerSo _playerSo;
        private Vector2 _moveDirection;
        private bool _inputEnable = true;
        private float _moveSpeed;
        private INoiseController _playerNoise;

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
            _playerNoise.IncreaseNoise();
        }

        public Vector2 GetMoveDirection() =>
            _moveDirection;
        
        private void Start()
        {
            _playerSo = Resources.Load<PlayerSo>(AssetPath.PLAYERSO_PATH);
            _moveSpeed = _playerSo.Speed;
            _playerNoise = AllServices.Container.Single<INoiseController>();
        }

        private void Update()
        {
            if (_inputEnable)
            {
                if (_moveDirection == Vector2.zero)
                {
                    AnimatorController.SetState(0);
                    _playerNoise.ReductionNoise();
                    return;
                }
                
                AnimatorController.SetState(1);
                Move(_moveDirection);
            }
        }

        private void Move(Vector2 direction)
        {
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
            transform.position += moveDirection * (_moveSpeed * Time.deltaTime);
        }
    }
}
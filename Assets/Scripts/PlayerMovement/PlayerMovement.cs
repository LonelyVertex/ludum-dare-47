﻿using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D = default;
    
    [SerializeField] private float _moveSpeed = default;
    [SerializeField] private float _dashMultiplier = default;
    
    [Inject] private PlayerInputState _playerInputState = default;
    
    private Camera _camera;
    private bool wantsToDash;
    
    private void Awake()
    {
        _camera = Camera.main;

        _playerInputState.dashEvent += HandleDashEvent;
    }

    private void OnDestroy()
    {
        _playerInputState.dashEvent -= HandleDashEvent;
    }

    private void HandleDashEvent()
    {
        wantsToDash = true;
    }

    private void FixedUpdate()
    {
        var direction = _camera.ScreenToWorldPoint(_playerInputState.mousePosition) - transform.position;

        _rigidbody2D.velocity = _playerInputState.movement * _moveSpeed;
        _rigidbody2D.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (wantsToDash)
        {
            _rigidbody2D.velocity *= _dashMultiplier;
            wantsToDash = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerGunRotator : MonoBehaviour
{
    [SerializeField] private Transform _gunTransform = default;
    
    [Inject] private PlayerInputState _playerInputState = default;

    private Camera _camera;
    
    void Awake()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        var direction = _camera.ScreenToWorldPoint(_playerInputState.mousePosition) - transform.position;

        _gunTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}

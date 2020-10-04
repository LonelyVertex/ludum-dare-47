using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator = default;
    [SerializeField] private Rigidbody2D _rigidbody2D = default;
    
    [Inject] private PlayerInputState _playerInputState = default;

    
    private readonly int velocityId = Animator.StringToHash("Velocity");
    private readonly int directionId = Animator.StringToHash("Direction");
    
    void Update()
    {
        _animator.SetFloat(velocityId, _rigidbody2D.velocity.magnitude);
        _animator.SetFloat(directionId, _playerInputState.movement.x);
    }
}

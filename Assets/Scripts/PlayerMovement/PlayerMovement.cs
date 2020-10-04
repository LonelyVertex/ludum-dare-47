using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D = default;
    
    [SerializeField] private float _moveSpeed = default;
    [SerializeField] private float _dashMultiplier = default;
    [SerializeField] private float _slowMultiplier = default;
    
    [Inject] private PlayerInputState _playerInputState = default;
    
    private bool wantsToDash;
    private int _slowAreas;

    private float MoveSpeed => (_slowAreas > 0 ? _slowMultiplier : 1) * _moveSpeed;
    
    private void Awake()
    {
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
        _rigidbody2D.velocity = _playerInputState.movement * MoveSpeed;

        /* if (wantsToDash)
        {
            _rigidbody2D.velocity *= _dashMultiplier;
            wantsToDash = false;
        } */
    }

    public void EnterSlowArea()
    {
        _slowAreas++;
    }

    public void LeaveSlowArea()
    {
        _slowAreas--;
    }
}

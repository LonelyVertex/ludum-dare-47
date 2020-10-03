using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapons : MonoBehaviour
{
    [Inject] PlayerInputState _playerInputState;
    int currentAmmoCount;
    public Transform bulletStart;
    public GameObject regularSeed;

    private void Awake()
    {
        _playerInputState.fireEvent += HandleFire;
    }

    private void HandleFire()
    {
        GameObject.Instantiate(regularSeed, bulletStart.position, transform.rotation);
    }

    void Start()
    {
        currentAmmoCount = 3;
    }

    void LowerAmmoCount()
    {
        currentAmmoCount -= 1;
    }
}

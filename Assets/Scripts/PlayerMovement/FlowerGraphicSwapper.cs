using UnityEngine;

public class FlowerGraphicSwapper : MonoBehaviour
{
    [SerializeField] private GameObject _fireFlower = default;
    [SerializeField] private GameObject _poisonFlower = default;
    [SerializeField] private GameObject _electricFlower = default;
    [SerializeField] private GameObject _piercingFlower = default;

    public void SwapGraphics(PlayerFlowerType type)
    {    
        _fireFlower.SetActive(false);
        _poisonFlower.SetActive(false);
        _electricFlower.SetActive(false);
        _piercingFlower.SetActive(false);
    
        switch (type)
        {
            case PlayerFlowerType.Fire:
                _fireFlower.SetActive(true);
                break;
            case PlayerFlowerType.Poison:
                _poisonFlower.SetActive(true);
                break;
            case PlayerFlowerType.Electric:
                _electricFlower.SetActive(true);
                break;
            case PlayerFlowerType.Piercing:
                _piercingFlower.SetActive(true);
                break;
        }
    }
}

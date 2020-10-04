using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFlowerButton : MonoBehaviour
{
    [SerializeField] private Button _selectButton = default;
    [SerializeField] private Image _flowerImage = default;
    [SerializeField] private TextMeshProUGUI _flowerText = default;
    
    public event System.Action<PlayerFlowerType> playerFlowerSelectedEvent;

    public void SetData(PlayerFlowerSO playerFlower)
    {
        _flowerImage.sprite = playerFlower.flowerSprite;
        _flowerText.text = $"{playerFlower.playerFlowerType}";

        _selectButton.onClick.RemoveAllListeners();
        _selectButton.onClick.AddListener(() =>
        {
            playerFlowerSelectedEvent?.Invoke(playerFlower.playerFlowerType);
        });
    }
}

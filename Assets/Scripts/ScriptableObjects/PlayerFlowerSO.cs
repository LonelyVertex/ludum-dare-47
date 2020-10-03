using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Flower")]
public class PlayerFlowerSO : ScriptableObject
{
    public PlayerFlowerType playerFlowerType;
    // Sprite for now
    public Sprite flowerSprite;
    public int maxHealth;
}

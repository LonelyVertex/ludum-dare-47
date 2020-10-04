public class BeetleLevelScaler : EnemyLevelScaler
{
    public Health health;
    public EnemyMeleeAttack attack;
    
    private const int BaseHealth = 150;
    private const int BaseAttackDamage = 40;
    
    public override void SetLevel(int level)
    {
        health.SetMaxHealth(Scaler.ScaleHealth(BaseHealth, level));
        attack.damage = Scaler.ScaleDamage(BaseAttackDamage, level);
    }
}

public class SpiderQueenLevelScaler : EnemyLevelScaler
{
    public Health health;
    public EnemyRangedAttack attack;
    public SpiderQueenController spiderQueen;

    private const int BaseHealth = 500;
    private const int BaseAttackDamage = 30;
    private const float BaseAttackTimer = 3.5f;
    private const float AttackCooldownLimit = 0.3f;
    private const float BasePoolTimer = 5;
    private const float PoolCooldownLimit = 0.4f;
    
    public override void SetLevel(int level)
    {
        health.SetMaxHealth(Scaler.ScaleHealth(BaseHealth, level));
        attack.damage = Scaler.ScaleDamage(BaseAttackDamage, level);
        attack.timer = Scaler.ScaleTimer(BaseAttackTimer, AttackCooldownLimit, level);
        spiderQueen.poolCooldown = Scaler.ScaleTimer(BasePoolTimer, PoolCooldownLimit, level);
    }
}

public class SpiderLevelScaler : EnemyLevelScaler
{
    public Health health;
    public EnemyRangedAttack attack;

    private const int BaseSpawnCount = 2;
    private const int BaseHealth = 80;
    private const int BaseAttackDamage = 20;
    private const float BaseAttackTimer = 4;
    private const float CooldownLimit = 0.3f;
    
    public override int SpawnCount(int level)
    {
        return Scaler.ScaleCount(BaseSpawnCount, level);
    }

    public override void SetLevel(int level)
    {
        health.SetMaxHealth(Scaler.ScaleHealth(BaseHealth, level));
        attack.damage = Scaler.ScaleDamage(BaseAttackDamage, level);
        attack.timer = Scaler.ScaleTimer(BaseAttackTimer, CooldownLimit, level);
    }
}
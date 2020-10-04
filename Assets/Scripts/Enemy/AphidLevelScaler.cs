public class AphidLevelScaler : EnemyLevelScaler
{
    public Health health;
    public EnemyMeleeAttack attack;

    private const int BaseSpawnCount = 3;
    private const int BaseHealth = 50;
    private const float BaseAttackTimer = 2;
    private const float CooldownLimit = 0.6f;

    public override int SpawnCount(int level)
    {
        return Scaler.ScaleCount(BaseSpawnCount, level);
    }

    public override void SetLevel(int level)
    {
        health.SetMaxHealth(Scaler.ScaleHealth(BaseHealth, level));
        attack.timer = Scaler.ScaleTimer(BaseAttackTimer, CooldownLimit, level);
    }
}

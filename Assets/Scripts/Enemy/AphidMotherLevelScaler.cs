public class AphidMotherLevelScaler : EnemyLevelScaler
{
    public Health health;
    public EnemyMeleeAttack attack;
    public AphidMotherController aphidMother;

    private const int BaseHealth = 150;
    private const float BaseAttackTimer = 2;
    private const float AttackCooldownLimit = 0.6f;
    private const float BaseAphidSpawnTimer = 5;
    private const float SpawnCooldownLimit = 0.4f;
    
    private int _level;

    public int Level => _level;

    public override int SpawnCount(int level)
    {
        return 1;
    }

    public override void SetLevel(int level)
    {
        _level = level;

        health.SetMaxHealth(Scaler.ScaleHealth(BaseHealth, level));
        attack.timer = Scaler.ScaleTimer(BaseAttackTimer, AttackCooldownLimit, level);
        aphidMother.spawnCooldown = Scaler.ScaleTimer(BaseAphidSpawnTimer, SpawnCooldownLimit, level);
    }
}
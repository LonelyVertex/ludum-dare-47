using UnityEngine;

public class AphidMotherLevelScaler : EnemyLevelScaler
{
    public Health health;
    public EnemyMeleeAttack attack;
    public AphidMotherController aphidMother;

    private const int BaseHealth = 150;
    private const float BaseAttackTimer = 2;
    private const float BaseAphidSpawnCooldown = 5;

    private int _level;

    public int Level => _level;

    public override int SpawnCount(int level)
    {
        return 1;
    }

    public override void SetLevel(int level)
    {
        _level = level;

        health.SetMaxHealth(ScaleHealth(level));
        attack.timer = ScaleAttackTimer(level);
        aphidMother.spawnCooldown = ScaleAphidSpawnCooldown(level);
    }

    private static int ScaleHealth(int level)
    {
        return Mathf.RoundToInt(BaseHealth + Mathf.Log(10 * (float) level));
    }

    private static float ScaleAttackTimer(int level)
    {
        return BaseAttackTimer - (-1 / ((float) level / 2) + 1);
    }

    private static float ScaleAphidSpawnCooldown(int level)
    {
        return BaseAphidSpawnCooldown - 2 * (-1 / ((float) level / 2) + 1);
    }
}
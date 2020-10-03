using UnityEngine;

public class AphidLevelScaler : EnemyLevelScaler
{
    public Health health;
    public EnemyMeleeAttack attack;

    private const int BaseSpawnCount = 3;
    private const int BaseHealth = 50;
    private const float BaseAttackTimer = 2;

    public override int SpawnCount(int level)
    {
        return Mathf.RoundToInt(BaseSpawnCount + Mathf.Log(level));
    }

    public override void SetLevel(int level)
    {
        health.SetMaxHealth(ScaleHealth(level));
        attack.timer = ScaleAttackTimer(level);
    }

    private static int ScaleHealth(int level)
    {
        return Mathf.RoundToInt(BaseHealth + Mathf.Log(10 * (float) level));
    }

    private static float ScaleAttackTimer(int level)
    {
        return BaseAttackTimer - (-1 / ((float) level / 2) + 1);
    }
}
using UnityEngine;

public class SpiderLevelScaler : EnemyLevelScaler
{
    public Health health;
    public EnemyRangedAttack attack;

    private const int BaseSpawnCount = 2;
    private const int BaseHealth = 80;
    private const float BaseAttackDamage = 20;
    private const float BaseAttackTimer = 5;


    public override int SpawnCount(int level)
    {
        return Mathf.FloorToInt(BaseSpawnCount + Mathf.Log(level));
    }

    public override void SetLevel(int level)
    {
        health.SetMaxHealth(ScaleHealth(level));
        attack.damage = ScaleAttackDamage(level);
        attack.timer = ScaleAttackTimer(level);
    }

    private static int ScaleHealth(int level)
    {
        return Mathf.RoundToInt(BaseHealth + 2 * Mathf.Log(10 * (float) level));
    }

    private static int ScaleAttackDamage(int level)
    {
        return Mathf.CeilToInt(BaseAttackDamage + 2 * Mathf.Log(10 * level));
    }

    private static float ScaleAttackTimer(int level)
    {
        return BaseAttackTimer - (-1 / ((float) level / 2) + 1);
    }
}
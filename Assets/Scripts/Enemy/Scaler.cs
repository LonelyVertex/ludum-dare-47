using UnityEngine;

public static class Scaler
{
    public static int ScaleCount(int baseCount, int level)
    {
        return Mathf.RoundToInt(baseCount + Mathf.Log(level));
    }

    public static int ScaleHealth(int baseHealth, int level)
    {
        return Mathf.RoundToInt(baseHealth + baseHealth / 10 * (level - 1));
    }

    public static float ScaleTimer(float baseTimer, float limit, int level)
    {
        return baseTimer + baseTimer * limit * (1 / level - 1);
    }

    public static int ScaleDamage(int baseDamage, int level)
    {
        return Mathf.CeilToInt(baseDamage + (baseDamage / 10) * 2 * Mathf.Log(level));
    }
}
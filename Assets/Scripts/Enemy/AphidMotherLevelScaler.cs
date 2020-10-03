using UnityEngine;

public class AphidMotherLevelScaler : EnemyLevelScaler
{
    private int _level;

    public int Level => _level;
    
    public override void SetLevel(int level)
    {
        _level = level;
        
        // TODO
        Debug.Log("Scale Aphid Mother to level " + level);
    }
}

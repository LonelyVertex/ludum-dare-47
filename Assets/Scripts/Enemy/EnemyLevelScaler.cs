using UnityEngine;

public class EnemyLevelScaler : MonoBehaviour
{
    public virtual void SetLevel(int level)
    {
        Debug.LogWarning("EnemyLevelScaler should be implemented for the specific enemy");
    }
}

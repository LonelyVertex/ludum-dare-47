using UnityEngine;

public class TimeController : MonoBehaviour
{
    public AnimationCurve curve;

    // TODO - For debug only
    [Range(0, 5)]
    public int ammo;
    public int maxAmmo = 5;

    private int Ammo => ammo;
    private int MaxAmmo => maxAmmo;

    void Update()
    {
        if (maxAmmo > 0)
        {
            Time.timeScale = curve.Evaluate(time: (float) Ammo / MaxAmmo);
        }
    }
}

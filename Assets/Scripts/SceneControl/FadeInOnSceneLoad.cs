using UnityEngine;

public class FadeInOnSceneLoad : MonoBehaviour
{
    [SerializeField] private FadeInOutController _fadeInOutController = default;
    
    void Start()
    {
        _fadeInOutController.FadeIn();
    }
}

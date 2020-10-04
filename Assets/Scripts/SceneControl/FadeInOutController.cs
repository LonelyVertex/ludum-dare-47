using System.Collections;
using UnityEngine;

public class FadeInOutController : MonoBehaviour
{
    [SerializeField] private float _duration = default;
    [SerializeField] private CanvasGroup _fadeOutCanvas = default;

    public void FadeIn()
    {
        StartCoroutine(Fade(1.0f, 0.0f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0.0f, 1.0f));
    }
    
    public IEnumerator FadeInCoroutine()
    {
        yield return Fade(1.0f, 0.0f);
    }

    public IEnumerator FadeOutCoroutine()
    {
        yield return Fade(0.0f, 1.0f);
    }

    IEnumerator Fade(float from, float to)
    {
        _fadeOutCanvas.gameObject.SetActive(true);
        
        var current = 0.0f;

        _fadeOutCanvas.alpha = from;

        yield return null;
        
        while (current < _duration)
        {
            _fadeOutCanvas.alpha = Mathf.Lerp(from, to, current / _duration);
            
            current += Time.unscaledDeltaTime;

            yield return null;
        }

        _fadeOutCanvas.alpha = to;
        
        if (_fadeOutCanvas.alpha <= 0.0f)
        {
            _fadeOutCanvas.gameObject.SetActive(false);
        }
    }
}

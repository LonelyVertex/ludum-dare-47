using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameScenesController : MonoBehaviour
{
    [Inject] private FadeInOutController _fadeInOutController = default;

    [Space]
    [Scene] public string menuScene;
    [Scene] public string gameScene;

    public void ToMenu()
    {
        TransitionToScene(menuScene);
    }

    public void ToGame()
    {
        TransitionToScene(gameScene);
    }
    
    public void TransitionToScene(string scene)
    {
        StartCoroutine(TransitionToSceneCoroutine(scene));
    }

    private IEnumerator TransitionToSceneCoroutine(string scene)
    {
        yield return _fadeInOutController.FadeOutCoroutine();

        SceneManager.LoadScene(scene);
    }
}

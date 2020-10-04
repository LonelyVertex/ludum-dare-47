using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameScenesController : MonoBehaviour
{
    [Inject] private FadeInOutController _fadeInOutController = default;

    [Space]
    [Scene] public string menuScene;

    public SceneSO[] scenes;
    
    public void ToMenu()
    {
        TransitionToScene(menuScene);
    }

    public void ToGame(int level)
    {
        var scene = GetScene(level);        
        TransitionToScene(scene.scenePath);
    }

    public SceneSO GetScene(int level)
    {
        return scenes[level % scenes.Length];
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

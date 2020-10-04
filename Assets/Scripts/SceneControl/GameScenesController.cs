using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameScenesController : MonoBehaviour
{
    [Inject] private FadeInOutController _fadeInOutController = default;

    [Space]
    [Scene] public string menuScene;

    [Scene] public string aphidScene;
    [Scene] public string spiderScene;
    [Scene] public string beetleScene;
    
    
    public void ToMenu()
    {
        TransitionToScene(menuScene);
    }

    public void ToGame(int level)
    {
        string scene = null;
        switch (level % 3)
        {
            case 1:
                scene = aphidScene;
                break;
            case 2:
                scene = spiderScene;
                break;
            case 3:
                scene = beetleScene;
                break;
        }
        
        TransitionToScene(scene);
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

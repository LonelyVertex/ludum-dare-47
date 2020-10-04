using UnityEngine;

[CreateAssetMenu(menuName = "SceneSO")]
public class SceneSO : ScriptableObject
{
    [Scene]
    public string scenePath;
    public Sprite scenePreview;
}

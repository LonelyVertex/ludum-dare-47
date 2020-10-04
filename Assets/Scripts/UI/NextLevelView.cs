using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelView : MonoBehaviour
{
    [SerializeField] private Image levelImage = default;
    
    public void Set(SceneSO scene)
    {
        levelImage.sprite = scene.scenePreview;
    }
}

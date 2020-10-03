using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup = default;

    public CanvasGroup canvasGroup => _canvasGroup;
}

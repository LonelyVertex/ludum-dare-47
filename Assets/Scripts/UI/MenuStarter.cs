using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStarter : MonoBehaviour
{
    [SerializeField] private UIPanelController _uiPanelController = default;
    
    void Start()
    {
        _uiPanelController.ShowMenu();
    }
}

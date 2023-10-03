using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlinkoButtonSizer : MonoBehaviour
{
    [SerializeField] Vector2 landScapeCellSize;
    [SerializeField] Vector2 portraitCellSize;
    [SerializeField] private GridLayoutGroup buttonGridLayout;

    private ScreenOrientation _currentScreenOrientation;

    private void Start()
    {
        _currentScreenOrientation = Screen.orientation;
        
        switch (_currentScreenOrientation)
        {
            case ScreenOrientation.Portrait: buttonGridLayout.cellSize = portraitCellSize;
                break;
            case ScreenOrientation.PortraitUpsideDown: buttonGridLayout.cellSize = portraitCellSize;
                break;
            case ScreenOrientation.LandscapeLeft: buttonGridLayout.cellSize = landScapeCellSize;
                break;
            case ScreenOrientation.LandscapeRight: buttonGridLayout.cellSize = landScapeCellSize;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.orientation != _currentScreenOrientation)
        {
            _currentScreenOrientation = Screen.orientation;

            switch (_currentScreenOrientation)
            {
                case ScreenOrientation.Portrait: buttonGridLayout.cellSize = portraitCellSize;
                    break;
                case ScreenOrientation.PortraitUpsideDown: buttonGridLayout.cellSize = portraitCellSize;
                    break;
                case ScreenOrientation.LandscapeLeft: buttonGridLayout.cellSize = landScapeCellSize;
                    break;
                case ScreenOrientation.LandscapeRight: buttonGridLayout.cellSize = landScapeCellSize;
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager
{
    public bool IsPaused { get { return isPaused; } set { isPaused = value; UpdatePauseHandlers(); } }

    private bool isPaused;

    public delegate void PauseHandler(bool isPaused);
    public event PauseHandler PauseStateChaned;

    public void TogglePause()
    {
        Debug.Log("toggle pause");
        IsPaused = !IsPaused;
    }

    List<IPauseHandler> pauseHandlers;
    public void UpdatePauseHandlers()
    {
        foreach(IPauseHandler ph in pauseHandlers)
        {
            ph.SetPaused(isPaused);
        }
    }

}

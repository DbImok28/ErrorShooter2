using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager
{
    public bool IsPaused { get { return isPaused; } set { isPaused = value; Debug.Log($"is paused : {isPaused}"); UpdatePauseHandlers(); } }

    private bool isPaused=false;

    private List<IPauseHandler> pauseHandlers=new List<IPauseHandler>();

    //public delegate void PauseHandler(bool isPaused);
    //public UnityEvent<bool> PauseStateChaned;
    public void SetPaused(bool pause)
    {
        IsPaused = pause;
    }


    public void AddPauseHandler(IPauseHandler pauseHandler)
    {
        pauseHandlers.Add(pauseHandler);

        //Debug.Log($"added pause haandler. count is {pauseHandlers.Count}");
    }

    public void UpdatePauseHandlers()
    {
        
        foreach(IPauseHandler ph in pauseHandlers)
        {
            ph.SetPaused(IsPaused);
        }
    }

}

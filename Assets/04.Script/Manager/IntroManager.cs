using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    private PlayableDirector playableDirector;

    private void Awake()
    {
        playableDirector = gameObject.GetComponent<PlayableDirector>();
    }
    
    public void StartBtn()
    {
        if (playableDirector.time >= playableDirector.duration)
        {
            SceneManager.LoadScene("Play");
        }
    }

    public void SkipBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void ExitBtn()
    {

    }
}

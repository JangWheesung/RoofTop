using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject name;
    [SerializeField] private GameObject btns;
    [SerializeField] private GameObject skipBtn;

    private PlayableDirector playableDirector;

    private void Awake()
    {
        playableDirector = gameObject.GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        IntroEnd();
        //Debug.Log($"playableDirector.time : {playableDirector.time}");
        Debug.Log($"playableDirector.duration : {playableDirector.duration}");
    }

    void IntroEnd()
    {
        if (playableDirector.time >= playableDirector.duration - 1.5f)
        {
            Debug.Log("???");
            SceneManager.LoadScene("Play");
        }
    }
    
    public void StartBtn()
    {
        name.SetActive(false);
        btns.SetActive(false);
        skipBtn.SetActive(true);

        playableDirector.Play();
    }

    public void SkipBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectToGame : MonoBehaviour
{
    private string selectSong = null;
    public AudioSource frozen;
    public AudioSource music2;

    public void FrozenEyes()
    {
        selectSong = "FrozenEyes";
        music2.Stop();
        frozen.Play();
    }
    public void Music2()
    {
        selectSong = "Music2";
        frozen.Stop();
        music2.Play();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(selectSong);
    }

    void Start()
    {
        FrozenEyes();
    }
}

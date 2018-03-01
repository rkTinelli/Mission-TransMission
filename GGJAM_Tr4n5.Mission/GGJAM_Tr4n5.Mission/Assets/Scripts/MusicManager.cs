using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioSource Music;

    public AudioClip Menu_track;
    public AudioClip Game_track;

    Scene scene;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
            
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Music.clip = Menu_track;
        Music.Play();
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Game" && Music.clip == Menu_track)
        {
               Music.clip = Game_track;
               Music.Play();
        }

        if (scene.name == "Menu" && Music.clip == Game_track)
        {
            Music.clip = Menu_track;
            Music.Play();
        }
    }
}

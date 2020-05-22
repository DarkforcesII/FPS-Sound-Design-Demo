using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController :  Singleton<GameController> 
{
    public AudioClip[] musicLayerClips;
   
    private AudioSource musicSource1;
    private AudioSource musicSource2;
    private AudioSource musicSource3;

    float lerpSpeed1;
    float lerpSpeed2;
    public float fadeTime;

    protected GameController() { }

    private int mHealth = 100;
    private int mTime = 60;

    private bool mGameOver = false;

    private void Awake()
    {
        musicSource1 = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        musicSource3 = this.gameObject.AddComponent<AudioSource>();

        musicSource1.clip = musicLayerClips[0];
        musicSource2.clip = musicLayerClips[1];
        musicSource3.clip = musicLayerClips[2];

        musicSource1.volume = 0.0f;
        musicSource2.volume = 0.0f;
        musicSource3.volume = 0.7f;

        musicSource1.loop = true;
        musicSource2.loop = true;
        musicSource3.loop = true;

        musicSource1.Play();
        musicSource2.Play();
        musicSource3.Play();
    }

    private void Start()
    {
        InvokeRepeating("Count", 0.0f, 1.0f);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (mHealth <= 80)
        {
            StartCoroutine(MusicLayer2FadeIn());
        }
        if (mHealth <= 50)
        {
            StartCoroutine(MusicLayer3FadeIn());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            mHealth = 50;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            mHealth = 80;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
            print("quit game");
        }
    }

    IEnumerator MusicLayer2FadeIn()
    {
        yield return null;

        while (musicSource1.volume < 0.7f)
        {
            lerpSpeed1 += Time.deltaTime;
            musicSource1.volume = Mathf.Lerp(0, 1, lerpSpeed1);
            yield return new WaitForSecondsRealtime(fadeTime);
        }
    }

    IEnumerator MusicLayer3FadeIn()
    {
        yield return null;

        while (musicSource2.volume < 0.7f)
        {
            lerpSpeed2 += Time.deltaTime;
            musicSource2.volume = Mathf.Lerp(0, 1, lerpSpeed1);
            yield return new WaitForSecondsRealtime(fadeTime);
        }
    }

    void Count()
    {
        if (mTime == 0)
        {
            mGameOver = true;
            CancelInvoke("Count");
        }
        else
        {
            mTime--;
        }
    }

    public void SetDamage(int damage)
    {
        if (mGameOver)
            return;

        mHealth -= damage;

        if (mHealth <= 0)
        {
            mHealth = 0;
            mGameOver = true;
            CancelInvoke("Count");
        }
    }

    public bool IsGameOver
    {
        get { return mGameOver; }
    }

    public int Health
    {
        get { return mHealth; }
    }

    public int Timer
    {
        get { return mTime; }
    }



}

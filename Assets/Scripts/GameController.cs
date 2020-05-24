using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController :  Singleton<GameController> 
{
    public SfxScript sfxScript;
    // add player grunts

    [Tooltip("This is where you will place your audio clips. Element 0 will be the track that plays at the start. " +
        "Element 1 will be the next layer to fade in and Element 2 will be the last.")]
    public AudioClip[] musicLayerClips;
   
    private AudioSource musicSource1;
    private AudioSource musicSource2;
    private AudioSource musicSource3;

    float lerpSpeed1;
    float lerpSpeed2;

    [Tooltip("This is the time in seconds that it takes for the music layers to fade in")]
    [SerializeField]
    float fadeTime;

    [Tooltip("This is the number that determines when the 2nd music layer will fade in. " +
        "For instance, if set to 80, the layer will fade in once the player's health drops to 80 or below. " +
        "You can also pres 1 to fade in the layer right away.")]
    [SerializeField]
    private int fadeParameter1;

    [Tooltip("This is the number that determines when the 2nd music layer will fade in. " +
        "For instance, if set to 80, the layer will fade in once the player's health drops to 80 or below. " +
        "You can also pres 2 to fade in the layer right away.")]
    [SerializeField]
    private int fadeParameter2;

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

        musicSource1.volume = 1;
        musicSource2.volume = 0.0f;
        musicSource3.volume = 0.0f;

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
        if (mHealth <= fadeParameter1)
        {
            StartCoroutine(MusicLayer2FadeIn());
        }
        if (mHealth <= fadeParameter2)
        {
            StartCoroutine(MusicLayer3FadeIn());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mHealth = fadeParameter1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mHealth = fadeParameter2;
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

        while (musicSource2.volume < 1)
        {
            lerpSpeed1 += Time.deltaTime;
            musicSource2.volume = Mathf.Lerp(0, 1, lerpSpeed1);
            yield return new WaitForSecondsRealtime(fadeTime);
        }
    }

    IEnumerator MusicLayer3FadeIn()
    {
        yield return null;

        while (musicSource3.volume < 1f)
        {
            lerpSpeed2 += Time.deltaTime;
            musicSource3.volume = Mathf.Lerp(0, 1, lerpSpeed1);
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

        // player hit coroutine
        StartCoroutine(DelayPlayerHitSound());

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

    IEnumerator DelayPlayerHitSound()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        sfxScript.PlayerHitSound();
    }

}

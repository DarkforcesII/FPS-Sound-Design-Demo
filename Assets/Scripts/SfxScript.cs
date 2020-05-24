using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxScript : MonoBehaviour
{
    public AudioClip bulletClip;
    public AudioClip[] enemyHitClips;
    public AudioClip[] playerHitClips;

    [Tooltip("This is what will control the pitch range of the gun clip.")]
    [SerializeField]
    private float gunPitchMin, gunPitchMax;

    // for enemy gun and player gun
    private AudioSource sfxSource1;
    // for enemy grunts
    private AudioSource sfxSource2;
    // for player grunts
    private AudioSource sfxSource3;

    // Start is called before the first frame update
    void Start()
    {
        sfxSource1 = this.gameObject.AddComponent<AudioSource>();
        sfxSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource3 = this.gameObject.AddComponent<AudioSource>();
    }

    public void ShootSound()
    {
        sfxSource1.clip = bulletClip;
        sfxSource1.pitch = Random.Range(gunPitchMin, gunPitchMax);
        sfxSource1.PlayOneShot(bulletClip);
    }

    public void EnemyHitSound()
    {
        sfxSource2.clip = enemyHitClips[Random.Range(0, enemyHitClips.Length)];
        sfxSource2.Play();
    }
    public void PlayerHitSound()
    {
        sfxSource3.clip = playerHitClips[Random.Range(0, enemyHitClips.Length)];
        sfxSource3.Play();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

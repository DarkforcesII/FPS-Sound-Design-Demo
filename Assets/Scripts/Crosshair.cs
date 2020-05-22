using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private int enemyHealth = 0;

    private AudioSource sfxSource1;
    private AudioSource sfxSource2;
    public AudioClip bulletClip;
    public AudioClip[] enemyHitClips;
    [SerializeField]
    private float pitchMin, pitchMax;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        sfxSource1 = this.gameObject.AddComponent<AudioSource>();
        sfxSource2 = this.gameObject.AddComponent<AudioSource>();
    }

    private void EnemyHitSFX()
    {
        sfxSource2.clip = enemyHitClips[Random.Range(0, enemyHitClips.Length)];
        sfxSource2.pitch = Random.Range(pitchMin, pitchMax);
        sfxSource2.Play();
    }

    private void ShootSFX()
    {
        sfxSource1.pitch = Random.Range(pitchMin, pitchMax);
        sfxSource1.PlayOneShot(bulletClip);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Input.mousePosition;
 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // player shoot sound
            ShootSFX();
            // Destroy Enemies
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    // enemy hit sound
                    EnemyHitSFX();
                    enemyHealth++;
                    print(enemyHealth);
                    if (enemyHealth == 3)
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public SfxScript sfxScript;

    private int enemyHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
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
            sfxScript.ShootSound();
            // Destroy Enemies
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    // enemy hit sound
                    sfxScript.EnemyHitSound();
                    enemyHealth++;
                    print(enemyHealth);
                    if (enemyHealth == 5)
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}

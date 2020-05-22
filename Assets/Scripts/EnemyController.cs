using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Soldier[] Enemies;

    // Start is called before the first frame update
    void Awake()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.IsGameOver)
            return;

        // check for active enemies
         foreach(Soldier enemy in Enemies)
        {
            if (enemy.IsActive)
                return;
        }

        int soldier = UnityEngine.Random.Range(0, Enemies.Length);
        Enemies[soldier].Activate();
    }
}

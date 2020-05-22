using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy_1 : MonoBehaviour
{

    internal void Hit()
    {
        Destroy(this.gameObject);
    }
}

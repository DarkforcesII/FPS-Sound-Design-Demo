using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy_2 : MonoBehaviour
{
    internal void Hit()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy_3 : MonoBehaviour
{
    internal void Hit()
    {
        Destroy(this.gameObject);
    }
}

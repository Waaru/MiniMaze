using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float life = 50;
    public float speed = 4f;
    //for later use - floating comportement
    [Range(0, 10)]
    public float pertinence = 5;
    public void Die()
    {
        Destroy(gameObject);
    }
}


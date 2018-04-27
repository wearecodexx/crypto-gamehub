using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    private float destroyTime;

    private void Awake()
    {
        destroyTime = Time.time + lifeSpan;
    }

    private void Update()
    {
        if (Time.time >= destroyTime)
            Destroy(this.gameObject);
    }
}

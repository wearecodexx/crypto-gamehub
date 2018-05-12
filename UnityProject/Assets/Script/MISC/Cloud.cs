using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private static readonly Range speeds = new Range(0.1f, 1f);

    private float speed;
    [SerializeField] private int direction;

    private void Start()
    {
        speed = Random.Range(speeds.min, speeds.max);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + direction * speed * Time.deltaTime, transform.position.y, 0);

        if (transform.position.x > Utility.ScreenWidth + 2 || transform.position.x - 2 < -Utility.ScreenWidth - 3)
        {
            direction *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
        }
    }
}
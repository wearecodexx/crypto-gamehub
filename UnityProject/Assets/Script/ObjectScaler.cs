using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [Range(0, 1)] public float percentage;
    private float scale;

    private Vector2 previousResolution;
    private Vector2 resolution;

    private void Update()
    {
        previousResolution = new Vector2(Screen.width, Screen.height);

        UpdatePercentage();

        scale = Utility.ScreenWidth * percentage;

        transform.localScale = Vector2.one * scale;
    }

    private void UpdatePercentage()
    {
        if (resolution != previousResolution)
        {
            resolution = new Vector2(Screen.width, Screen.height);

            if (resolution.y > resolution.x)
                percentage *= 2.5f;
        }
    }
}

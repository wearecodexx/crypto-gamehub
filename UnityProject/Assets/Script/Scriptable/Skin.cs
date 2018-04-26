using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skin : ScriptableObject
{
    public string name;
    public Sprite sprite;

    [Header("Collider Data")]
    public float radius;
    public Vector2 offset;
}

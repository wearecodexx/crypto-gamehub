using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public static int ActiveObjects { get; private set; }

    public static void ResetCount()
    {
        ActiveObjects = 0;
    }

    public static void ChangeObjectCount(int newCount)
    {
        ActiveObjects = newCount;
    }

    public Type objectType = Type.FRIENDLY;

    protected int multiplier;

    public abstract void Shoot(float force);

    public abstract void Explode();

    public abstract void Caught();

    protected void IncrementCount() // This function increments count by 1.
    {
        ActiveObjects++;
        multiplier = ActiveObjects;
    }

    protected void DecrementCount() // This function decrements count by 1.
    {
        if (ActiveObjects > 0)
            ActiveObjects--;
    }

    protected abstract void Destroy();
}

public enum Type { HOSTILE, FRIENDLY}
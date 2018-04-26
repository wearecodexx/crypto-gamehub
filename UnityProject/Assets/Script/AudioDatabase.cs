using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDatabase : MonoBehaviour
{
    public static AudioDatabase instance;

    private AudioSource[] levelSFX;
    public int sfxCount { get; private set; }

    private void Awake()
    {
        instance = this;

        Initialize();
    }

    private void Initialize()
    {
        levelSFX = FindObjectsOfType<AudioSource>();

        sfxCount = levelSFX.Length;
    }

    public AudioSource GetSFX(int ID)
    {
        return levelSFX[ID];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public static ObstacleGenerator instance;

    private void OnEnable()
    {
        GameManager.GameStateChanged += ApplyGameoverEffects;
    }
    private void OnDisable()
    {
        GameManager.GameStateChanged -= ApplyGameoverEffects;
    }

    [SerializeField] private InteractableObject[] objects = new InteractableObject[] { };
    private InteractableObject previousObject;

    [SerializeField] private Range force;

    private Vector2 pos;

    [SerializeField] Transform parent;

    [Header("Interval Decay")]
    private float elapsedTime;
    [SerializeField] private float rate;
    [SerializeField] private float baseInterval;
    [SerializeField] private float minInterval;
    private float interval;
    private float spawnTime;

    [Header("Settings for Gameover Effects")]
    [SerializeField] private Range gameoverForceRange;
    [SerializeField] private float gameoverMinInterval;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (GameManager.currentState != GameState.INACTIVE)
        {
            UpdateInterval();

            if (elapsedTime > spawnTime)
                GenerateObstacle();
        }
    }

    private void Initialize()
    {
        spawnTime = 4f;
        interval = baseInterval;
    }

    private void UpdateInterval()
    {
        elapsedTime += Time.deltaTime;

        if (interval > minInterval)
            interval = baseInterval - rate * Mathf.Pow(elapsedTime, 0.5f);
        else if (interval == minInterval)
            return;
        else
            interval = minInterval;
    }

    private void GenerateObstacle()
    {
        previousObject = objects[Random.Range(0, objects.Length)];

        pos = new Vector2(Random.Range(-Utility.ScreenWidth, Utility.ScreenWidth), Utility.ScreenHeight);

        previousObject = Instantiate(previousObject, pos, Quaternion.identity, parent).GetComponent<InteractableObject>();
        previousObject.Shoot(Random.Range(force.min, force.max));

        spawnTime = elapsedTime + interval;
    }

    private void ApplyGameoverEffects()
    {
        if (GameManager.currentState == GameState.AWAITING)
        {
            this.force = gameoverForceRange;
            this.minInterval = gameoverMinInterval;
        }
    }
}

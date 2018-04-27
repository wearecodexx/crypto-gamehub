using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GenericObject : InteractableObject
{
    public static readonly int baseScore = 3;

    private Skin initialSkin;
    private Skin hostileSkin;

    #region Rotate Animations
    public Range speedRange;
    private float rotationSpeed;
    private int direction;
    #endregion

    #region SFX
    #endregion 

    [SerializeField] private ParticleSystem blood;

    #region References.
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CircleCollider2D coll;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (rb.velocity.y > 3 && objectType != Type.HOSTILE)
            Transform();

        if (objectType == Type.HOSTILE && transform.position.y >= Utility.ScreenHeight)
            Destroy();

        Rotate();
    }

    private void Initialize()
    {
        initialSkin = SkinDatabase.GetRandom(Type.FRIENDLY);
        hostileSkin = SkinDatabase.GetRandom(Type.HOSTILE);

        ApplySkin(initialSkin);

        rotationSpeed = Random.Range(speedRange.min, speedRange.max);

        direction = Random.Range(0, 2);
        if (direction == 0)
            direction = 1;
        else
            direction = -1;
    }

    private void ApplySkin(Skin skin)
    {
        this.name = skin.name;
        sr.sprite = skin.sprite;
        coll.offset = skin.offset;
        coll.radius = skin.radius;
    }

    public override void Shoot(float force)
    {
        IncrementCount();
        rb.AddForce(Vector2.down * force, ForceMode2D.Impulse);
    }

    private void Transform()
    {
        objectType = Type.HOSTILE;
        ApplySkin(hostileSkin);
    }

    public override void Explode()
    {
        DecrementCount();
        Destroy();
    }

    public override void Caught()
    {
        AudioDatabase.instance.GetSFX(Random.Range(0, AudioDatabase.instance.sfxCount)).Play();
        ScoreSystem.instance.IncrementScore(baseScore * multiplier);

        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.forward * direction * rotationSpeed * Time.deltaTime);
    }

    protected override void Destroy()
    {
        DecrementCount();
        Destroy(this.gameObject);
    }
}

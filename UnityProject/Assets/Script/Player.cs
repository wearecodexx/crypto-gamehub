using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.currentState == GameState.ACTIVE)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        transform.position = Utility.MousePos;
    }

    private void CaughtFriendlyObject(InteractableObject _object)
    {
        _object.Caught();
    }

    private void Die()
    {
        StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        AnimationClip animation = anim.runtimeAnimatorController.animationClips[1];
        float length = animation.length;

        anim.Play(animation.name);

        yield return new WaitForSeconds(length);

        this.gameObject.SetActive(false);

        GameManager.instance.Gameover();
    }

    private InteractableObject collision;
    private void OnTriggerEnter2D(Collider2D other)
    {
        collision = other.gameObject.GetComponent<InteractableObject>();

        if (collision != null)
        {
            if (collision.objectType == Type.FRIENDLY)
            {
                CaughtFriendlyObject(collision);
            }
            else
            {
                collision.Explode();
                Die();
            }
        }
    }
}

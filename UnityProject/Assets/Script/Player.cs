using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private void Update()
    {
        UpdatePosition2();

        transform.position = targetPos;
    }

    private Vector2 targetPos;

    private void UpdatePosition2()
    {
        targetPos = Utility.MousePos;
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
        this.gameObject.SetActive(false);

        GameManager.instance.Gameover();
    }

    //private void OnMouseDrag()
    //{
    //    if (GameManager.currentState == GameState.ACTIVE)
    //    {
    //        UpdatePosition();
    //    }
    //}

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigIcon : MonoBehaviour
{
    [SerializeField] private string[] phrases;
    [SerializeField] private Text iconText;

    private void OnEnable()
    {
        GameManager.GameStateChanged += UpdateIcon;
    }

    private void OnDisable()
    {
        GameManager.GameStateChanged -= UpdateIcon;
    }

    private void UpdateIcon()
    {
        iconText.text = phrases[Random.Range(0, phrases.Length)];
    }
}

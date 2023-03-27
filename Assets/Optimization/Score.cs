using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TMP_Text text;
    int score;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    private void OnEnable() => Spawner.onSpawned += IncreaseScore;
    private void OnDisable() => Spawner.onSpawned -= IncreaseScore;

    private void IncreaseScore()
    {
        score++;
        text.text = $"Score: {score}";
    }
}

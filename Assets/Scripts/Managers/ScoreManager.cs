using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [SerializeField] TextMeshProUGUI scoreText;

    private float score;

    private float passingRate = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        scoreText.text = "0";
        score = 0;
    }

    public void AddScore(float value)
    {
        score += value;
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public float ReturnScore()
    {
        return score;
    }
}

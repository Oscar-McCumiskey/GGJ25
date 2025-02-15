using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [SerializeField] TextMeshProUGUI scoreText;

    private float score;
    private float totalScore;

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
        totalScore = 0;
    }

    public void AddScore(float value)
    {
        score += value;
    }

    public void UpdateScore()
    {
        scoreText.text = totalScore.ToString();
    }

    public float ReturnScore()
    {
        return (totalScore / 3000) * 100;
    }

    public float AddScore()
    {
        score = 0;
        var drink = OrderManager.Instance.currentOrder;

        //SCORE MATHS
        if (drink.correctCupSize && drink.isThereACup)
        {
            score += 100;
        }
        else
        {
            Debug.Log("NO CUP OR WRONG CUP SIZE");
        }

        if (drink.correctTapioca)
        {
            score += 100;
        }
        else
        {
            Debug.Log("WRONG TAPIOCA");
        }

        if (drink.isCupShaken)
        {
            score += 100;
        }

        if (drink.correctMilkType)
        {
            score += 100;
        }
        else
        {
            Debug.Log("WRONG MILK OR NO MILK");
        }

        totalScore += score;
        return score;
    }
}

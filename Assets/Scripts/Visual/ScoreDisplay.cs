using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/*
 * ScoreDisplay
 * Wessel Roelofse
 * 01/04/2022
 * 
 * Score Display
 */
[RequireComponent(typeof(TMP_Text))]
public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text scoretext;
    private int currentScore;
    void Start()
    {
        scoretext = GetComponent<TMP_Text>();
    }

    void Update()
    {
        currentScore = Mathf.CeilToInt(Mathf.Lerp(currentScore,GameController.Instance.score, 10f * Time.deltaTime));
        scoretext.text = currentScore.ToString();    
    }
}

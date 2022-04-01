using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/*
 * ScoreDisplayEnd
 * Wessel Roelofse
 * 01/04/2022
 * 
 * End Score Display
 */
[RequireComponent(typeof(TMP_Text))]
public class ScoreDisplayEnd : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = "SCORE: " + GameController.Instance.score.ToString();
    }
}

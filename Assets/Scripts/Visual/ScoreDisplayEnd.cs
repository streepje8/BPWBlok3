using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreDisplayEnd : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = "SCORE: " + GameController.Instance.score.ToString();
    }
}

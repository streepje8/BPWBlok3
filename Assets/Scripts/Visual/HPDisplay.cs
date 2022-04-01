using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/*
 * HPDisplay
 * Wessel Roelofse
 * 01/04/2022
 * 
 * Health Display
 */
[RequireComponent(typeof(TMP_Text))]
public class HPDisplay : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = GameController.Instance.playerstats.shields.ToString() + "/" + GameController.Instance.playerstats.HP.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * BackToMainButton
 * Wessel Roelofse
 * 01/04/2022
 * 
 * Back to main button click code
 */
public class BackToMainButton : MonoBehaviour
{
    public void click()
    {
        SceneManager.LoadScene(0);
    }
}

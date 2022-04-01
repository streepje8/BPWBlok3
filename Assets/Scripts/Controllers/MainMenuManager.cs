using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button continueButton;

    private void Start()
    {
        Savedata.SaveFile =
#if UNITY_EDITOR
            Application.dataPath + "/savefile.dat";
#else
        Application.persistentDataPath + "/savefile.dat";
#endif
        continueButton.interactable = File.Exists(Savedata.SaveFile);
    }
    public void New()
    {
        if (File.Exists(Savedata.SaveFile))
        {
            File.Delete(Savedata.SaveFile);
        }
        Continue();
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

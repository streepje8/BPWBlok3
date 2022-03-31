using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void New()
    {
        Savedata.SaveFile =
#if UNITY_EDITOR
            Application.dataPath + "/savefile.dat";
#else
        Application.persistentDataPath + "/savefile.dat";
#endif
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

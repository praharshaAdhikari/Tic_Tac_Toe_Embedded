using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void ScoreReset()
    {   
        string fpath = Application.dataPath + "\\scores.txt";
        File.Exists(@fpath);
        string[] lines = new string[]
        {
            "000",
            "000"
        };
        File.WriteAllLines(fpath, lines);
        SceneChanger();
    }
    public void SceneChanger()
    {
        SceneManager.LoadScene("Tic Tac Toe");
    }
}

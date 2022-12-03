using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using IronPython.Hosting;

public class GameController : MonoBehaviour
{
    public Text[] buttonTexts;
    public bool player = true, training = false ,win = false;
    public string XO;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Text computerScore;
    public Text playerScore;
    public Button train;
    public Text trainText;
    private int moves;
    private string fpath, csv;
    private float delay;
    private int[] movesList = new int[9];
    private int m=0;
    private char[] clicked_pos = new char[9];

    private void Awake()
    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
        delay = 5;
        gameOverPanel.SetActive(false);
        moves = 0;
        for (int i = 0; i<clicked_pos.Length; i++) clicked_pos[i] = '-';
        SetButtonsAsController();
        fpath = Application.dataPath + "\\scores.txt";
        csv = Application.dataPath + "\\data.csv";
        File.Exists(@fpath);
        File.Exists(@csv);
        string[] lines = File.ReadAllLines(@fpath);
        computerScore.text = lines[0];
        playerScore.text = lines[1];
        if (PlayerPrefs.GetInt("running")==1)
        {
            training = true;
            Color red = new Color(0.85f, 0.22f, 0.22f);
            train.image.color = red;
            trainText.text = "STOP";
            Update();
        }
    }

    private void Update()
    {
        if (!player && !training) ComputerMove();
        if (training){
            ComputerMove();
        }
    }

    private void SetButtonsAsController()
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            buttonTexts[i].GetComponentInParent<TextChanger>().SetButtonAsController(this);
        }
    }
    public void WinDraw()
    {
        moves++;
        if (buttonTexts[0].text == XO && buttonTexts[1].text == XO && buttonTexts[2].text == XO) GameOver();
        if (buttonTexts[3].text == XO && buttonTexts[4].text == XO && buttonTexts[5].text == XO) GameOver();
        if (buttonTexts[6].text == XO && buttonTexts[7].text == XO && buttonTexts[8].text == XO) GameOver();
        if (buttonTexts[0].text == XO && buttonTexts[3].text == XO && buttonTexts[6].text == XO) GameOver();
        if (buttonTexts[1].text == XO && buttonTexts[4].text == XO && buttonTexts[7].text == XO) GameOver();
        if (buttonTexts[2].text == XO && buttonTexts[5].text == XO && buttonTexts[8].text == XO) GameOver();
        if (buttonTexts[0].text == XO && buttonTexts[4].text == XO && buttonTexts[8].text == XO) GameOver();
        if (buttonTexts[2].text == XO && buttonTexts[4].text == XO && buttonTexts[6].text == XO) GameOver();

        if (moves >= 9 && !win)
        {
            Draw();
            return;
        }
        GetClickedPos();

        delay = 5;
    }
    private void GameOver()
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            if (buttonTexts[i].GetComponentInParent<TextChanger>().button.interactable == true)
            {
                ColorBlock cb = buttonTexts[i].GetComponentInParent<TextChanger>().button.colors;
                cb.disabledColor = Color.grey;
                buttonTexts[i].GetComponentInParent<TextChanger>().button.colors = cb;
                buttonTexts[i].GetComponentInParent<TextChanger>().button.interactable = false;
            }
        }
        ScoreKeeper();
        gameOverText.text = "'" + XO + "' WINS !";
        gameOverPanel.SetActive(true);
        win = true;
        if (training) SceneManager.LoadScene("Tic Tac Toe");
    }
    private void Draw()
    {
        gameOverText.text = "DRAW  '_'";
        gameOverPanel.SetActive(true);
        if (training) StoreMoves("0");
        if (training) SceneManager.LoadScene("Tic Tac Toe");
    }
    public void ScoreKeeper()
    {
        if (XO == "O")
        {
            int score = Convert.ToInt16(playerScore.text);
            score++;
            playerScore.text = Convert.ToString(score);
            if (training) StoreMoves("-1");
        }
        if (XO == "X")
        {
            int score = Convert.ToInt16(computerScore.text);
            score++;
            computerScore.text = Convert.ToString(score);
            if (training) StoreMoves("1");
        }
        string[] lines = new string[]
        {
            computerScore.text,
            playerScore.text
        };
        File.WriteAllLines(fpath, lines);
    }
    private void ComputerMove()
    {
        int i;
        delay += delay * Time.deltaTime;
        if (delay >= 8)
        {
            string info = new string(clicked_pos);
            if (training) i = UnityEngine.Random.Range(0, 9);
            else i = GetPos(info);
            if (buttonTexts[i].GetComponentInParent<TextChanger>().button.interactable)
            {
                movesList[m] = i+1;
                m++;
                buttonTexts[i].GetComponentInParent<TextChanger>().clicked();
            }
        }
    }
    private void StoreMoves(string result)
    {
        string score = "";
        for (int i=0; i < movesList.Length; i++) score += Convert.ToString(movesList[i]) + ",";
        StreamWriter sw = File.AppendText(@csv);
        sw.WriteLine(score + result);
        sw.Close();
    }
    public void Training()
    {   
        if (!training)
        {
            PlayerPrefs.SetInt("running", 1);
            training = true;
            Color red = new Color(0.85f, 0.22f, 0.22f);
            train.image.color = red;
            trainText.text = "STOP";
            Update();
        }
        else
        {
            PlayerPrefs.SetInt("running", 0);
            training = false;
            Color green = new Color(0.13f, 0.95f, 0.6f);
            train.image.color = green;
            trainText.text = "COLLECT DATA";
            Update();
        }
    }
    private void GetClickedPos()
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            if (buttonTexts[i].text == "O") clicked_pos[i] = 'O';
            if (buttonTexts[i].text == "X") clicked_pos[i] = 'X';
        }
    }
    private int GetPos(string board_info)
    {
        var engine = Python.CreateEngine();

        ICollection<string> searchPaths = engine.GetSearchPaths();

        //Path to the folder of greeter.py
        searchPaths.Add(Application.dataPath);
        //Path to the Python standard library
        searchPaths.Add(Application.dataPath + "\\Plugins\\Lib\\");
        engine.SetSearchPaths(searchPaths);

        dynamic py = engine.ExecuteFile(Application.dataPath + "\\embed.py");
        dynamic board = py.Board(board_info);

        return Convert.ToInt16(board.returnBestMove());
    }
}
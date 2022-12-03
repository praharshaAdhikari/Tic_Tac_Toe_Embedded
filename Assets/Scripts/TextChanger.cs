using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    private GameController gameController;

    public void clicked()
    {
        gameController.XO = "O";
        if (!gameController.player) changeTurn();

        buttonText.text = gameController.XO;
        button.interactable = false;
        gameController.WinDraw();
        gameController.player = !gameController.player;
    }
    public void SetButtonAsController(GameController controller)
    {
        gameController = controller;
    }
    private void changeTurn()
    {
        gameController.XO = "X";
        Color red = new Color(0.85f, 0.22f, 0.22f);
        ColorBlock cb = button.colors;
        cb.disabledColor = red;
        button.colors = cb;
    }
}
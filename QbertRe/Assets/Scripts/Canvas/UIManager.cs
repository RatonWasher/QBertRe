using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Text scoreText;
    public Text levelText;
    public Text livesText;
    public Image targetColorImg;

    private Color targetColor;
    private int newLifeMultiplier = 1;
    private int scoreForNewLife = 8000;
    private GameManager GM;



    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void addScore(int scoreToAdd)
    {
        GM.addScore(scoreToAdd);

        updateScoreUI(GM.getScore());

        if(GM.getScore() >= scoreForNewLife * newLifeMultiplier)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().addLife();
            newLifeMultiplier++;
        }
    }


    public void updateScoreUI(int _score)
    {
        scoreText.text = "Score : " + _score;
    }


    public void updateLevelUI(int world, int level)
    {
        levelText.text = "Level " + world + "-" + level;
    }


    public void updateColorUI(Color newColor)
    {
        targetColor = newColor;
        targetColorImg.GetComponent<Image>().color = targetColor;
    }


    public void updateLivesUI(int _lives)
    {
        livesText.text = "Lives : " + _lives;
    }
}

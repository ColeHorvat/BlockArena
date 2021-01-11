using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    public float playerScore;

    float playerHighScore;
    EnemyController enemyController;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI endText;

    // Start is called before the first frame update
    void Start()
    {
        endText.enabled = false;
        playerScore = 0;

        endText.enabled = false;

        playerHighScore = PlayerPrefs.GetFloat("playerHighScore");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")) Application.Quit();

        if(Input.GetKey("r"))  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if(playerScore > playerHighScore) playerHighScore = playerScore;

        scoreText.SetText("SCORE: " + playerScore);
        highScoreText.SetText("HIGH SCORE: " + playerHighScore);

        PlayerPrefs.SetFloat("playerHighScore", playerHighScore);

        if(playerScore > 50) enemyController.speed = 600f;
    }
}

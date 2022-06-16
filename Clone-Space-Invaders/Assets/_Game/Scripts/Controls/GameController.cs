using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    //* Private attributes
    private const float disableTouchTime = 2.0f;    // how long will touch be disabled
    private const float wait2Count = 0.5f;
    private float overtimeDisableTouch;             //stores elapsed time for touch
    private bool _touch;        // if true, enemies change direction after touching something
    private int score, lives;   // life and score values
    private int countEnemy;    // Number of enemies in the scene


    #region ENCAPSULATION
    public bool touch { get => _touch; set => _touch = value; }
    #endregion
    //* Public attributes

    //* Private methods
    void Awake()
    {
        // Assigning initial values
        touch = false;
        overtimeDisableTouch = 0;
        countEnemy = 0;
        score = 0;
        lives = 3;

        StartCoroutine(CountEnemies());     // Enemy counter coroutine
    }
    void FixedUpdate()
    {
        if (touch)
        {
            overtimeDisableTouch += Time.fixedDeltaTime;
            if (overtimeDisableTouch >= disableTouchTime)
            {
                overtimeDisableTouch = 0;
                touch = false;
            }
        }
    }

    /// <summary>
    /// called when the player loses the match
    /// </summary>
    private void GameOver()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// called when the player wins the match
    /// </summary>
    private void WinGame()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// A late coroutine that calculates the amount of enemies shortly after it has started
    /// </summary>
    private IEnumerator CountEnemies()
    {
        // Do anything before 'wait2Count'
        yield return new WaitForSeconds(wait2Count);

        Enemy[] enemies = FindObjectsOfType<Enemy>();   // Finds all enemies in the scene
        countEnemy = enemies.Length;        // Stores the amount of enemies found
    }


    //* Public methods

    /// <summary>
    /// Called by after clicking the start game button
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    /// <summary>
    /// Whenever the player takes damage the health values are changing
    /// </summary>
    public void UpdateLives()
    {
        if (lives == 0)     // If the total lives is 0, you lose the game
        {
            GameOver();
            return;
        }
        lives --;     // Decrease one in lives
        Master.Instance.txtLives.text = $"Lives: {lives.ToString()}";     //update UI to new lives value

    }

    /// <summary>
    /// Whenever the player takes damage the health values are changing
    /// </summary>
    public void UpdateScore(int value)
    {
        countEnemy --;      // decrease the number of enemies by one
        score += value;     // increases the score
        Master.Instance.txtScore.text = $"Score: {score.ToString()}";   //update UI to new score value

        if (countEnemy <= 0)    // If all enemies died, you won the game
            WinGame();
    }
}

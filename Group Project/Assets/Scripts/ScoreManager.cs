using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // To use ScoreManager.instance.MethodName() in scripts
    public static ScoreManager instance;

    public AudioSource music;
    public Transform UI;
    public TMP_Text coinText;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public PowerupText powerupText;

    private Transform gameUI;
    private Transform deadUI;
    private PlayerController player;
    private AudioSource coinSound;
    private TMP_Text deadScoreTitle;
    private TMP_Text deadScoreText;
    

    public float scoreToNextLife = 5000;

    private int coins = 0;
    private float score = 0f;
    private int scoreMultiplier = 1;
    private bool dead = false;

    private const float countDuration = 0.5f;
    private const float coinWorth = 50f;
    
    private void Start()
    {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        coinSound = GetComponent<AudioSource>();
        deadUI = UI.GetChild(0);
        gameUI = UI.GetChild(1);
        Transform deadScoreGroup = deadUI.GetChild(3);
        deadScoreTitle = deadScoreGroup.GetChild(0).GetComponent<TMP_Text>();
        deadScoreText = deadScoreGroup.GetChild(1).GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (dead) return;

        if (score >= scoreToNextLife)
        {
            scoreToNextLife += scoreToNextLife;
            grantLife(1);
        }

        score += player.GetSpeed() * Time.deltaTime * scoreMultiplier;
        scoreText.text = score.ToString("0");
        livesText.text = player.getLives().ToString();
    }

    public void Die()
    {
        if (dead) return;
        dead = true;
        Time.timeScale = 0.25f;
        music.enabled = false;

        deadUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);

        float totalScore = score + (coins * coinWorth);
        if (PlayerPrefs.GetFloat("HighScore", 0f) < totalScore)
        {
            PlayerPrefs.SetFloat("HighScore", totalScore);
            deadScoreTitle.text = "New High Score";
        }

        StartCoroutine(CountTo(totalScore));
    }

    IEnumerator CountTo(float target)
    {
        for (float timer = 0f; timer < countDuration; timer += Time.unscaledDeltaTime)
        {
            float progress = timer / countDuration;
            deadScoreText.text = (target * progress).ToString("0");
            yield return null;
        }
        deadScoreText.text = target.ToString("0");
    }

    public void Respawn()
    {
        // Prevent timescale affecting next scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Returns true if the player is alive to collect coins
    public bool AddCoins(int count)
    {
        if (dead) return false;
        if (coins + count *scoreMultiplier <= 0)
        {
            coins = 0;
          
        }
        else
        {
            coins += count * scoreMultiplier;
           
        }

        coinText.text = coins.ToString();

        player.AddSpeed(count * 0.5f);

        // Audio is played from here since the coin deletes itself and attached audio sources
        coinSound.Play();
        return true;
    }

    public void PowerUpPopup(string text)
    {
        powerupText.ShowText(text);
    }

    public void SpeedPowerUp(float multiplier, float time)
    {
        player.SetSpeedMultiplier(multiplier, time);
    }

    public void SetScoreMultiplier(int multi, float time)
    {
        scoreMultiplier = multi;
        Invoke("ResetScoreMultiplier", time);
    }

    public void grantLife(int amount)
    {
        player.addLives(amount);
    }
    private void ResetScoreMultiplier()
    {
        scoreMultiplier = 1;
    }
}

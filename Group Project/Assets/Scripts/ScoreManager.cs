using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text coinText;
    public TMP_Text scoreText;
    public AudioSource music;
    public GameObject UI;
    public TMP_Text deadScoreText;

    public PowerupText powerupTXT;

    private PlayerController player;
    private AudioSource coinSound;

    private int coins = 0;
    private float score = 0f;
    private float scoreMultiplier = 1.0f;
    private bool dead = false;
    private float countDuration = 0.5f;
    private float coinWorth = 50;
    
    private void Start()
    {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        coinSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (dead) return;
        score += (player.GetSpeed() * Time.deltaTime * scoreMultiplier);
        scoreText.text = score.ToString("0");
    }

    public void Die()
    {
        if (dead) return;

        dead = true;
        Time.timeScale = 0.25f;
        
        Transform deadUI = UI.transform.GetChild(0);
        Transform gameUI = UI.transform.GetChild(1);
        deadUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
        
        float totalScore = score + (coins * coinWorth);
        print(PlayerPrefs.GetFloat("highscore", 0));
        if (PlayerPrefs.GetFloat("highscore", 0) < totalScore)
        {
            PlayerPrefs.SetFloat("highscore", totalScore);
            Transform deadScoreGroup = deadUI.GetChild(3);
            deadScoreGroup.GetChild(0).gameObject.SetActive(true);
            deadScoreGroup.GetChild(1).gameObject.SetActive(false);
        }

        StartCoroutine("CountTo", coins);
        music.enabled = false;
    }

    IEnumerator CountTo(int target)
    {
        int start = 0;
        for (float timer = 0; timer < countDuration; timer += Time.deltaTime)
        {
            float progress = timer / countDuration;
            coins = (int)Mathf.Lerp(start, target, progress);
            deadScoreText.text = (score + (coins * coinWorth)).ToString("0");
            yield return null;
        }

        coins = target;
        deadScoreText.text = (score + (coins * coinWorth)).ToString("0");
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

        coins += count * (int)scoreMultiplier;
        coinText.text = coins.ToString();

        player.AddSpeed(count * 0.5f);

        // Audio is played from here, since the coin deletes itself and any audio sources attached
        coinSound.Play();

        return true;
    }

    public void powerUpPopUp(string text)
    {
        powerupTXT.showText(text);
    }

    public void speedPowerUp(float multiplier, float time)
    {
        player.setSpeedMultiplier(multiplier, time);
    }

    public void setScoreMultiplier(float multi, float time)
    {
        scoreMultiplier = multi;
        Invoke("resetScoreMultiplier", time);
    }

    public void resetScoreMultiplier()
    {
        scoreMultiplier = 1.0f;
    }

}

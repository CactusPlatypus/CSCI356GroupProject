using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public GameObject deadUI;
    public TMP_Text coinText;
    public TMP_Text scoreText;
    public AudioSource music;

    private PlayerController player;
    private AudioSource coinSound;

    private int coins = 0;
    private float score = 0f;
    private bool dead = false;

    private void Start()
    {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        coinSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (dead) return;
        // Score = distance = speed * time
        score += (player.getMovement() * Time.deltaTime);
        scoreText.text = score.ToString("0");
    }

    public void Die()
    {
        if (dead) return;

        dead = true;
        Time.timeScale = 0.25f;
        deadUI.SetActive(true);
        music.enabled = false;
    }

    public void Respawn()
    {
        // Prevent timescale affecting next scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    // Returns true if the player is alive to collect coins
    public bool AddCoins(int count)
    {
        if (dead) return false;

        coins += count;
        coinText.text = coins.ToString();

        player.AddSpeed(count * 0.5f);

        // Audio is played from here, since the coin deletes itself and any audio sources attached
        coinSound.Play();

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    private static Director singleton;

    public Spawner EnemySpawner;
    public Tutorial Tutorial;

    public Text Score;
    public CanvasGroup Group;
    public int PlayerScore = 0;
    public bool GameOver = false;

    private float time = 0.0f;

    private void Start() {
        singleton = this;
    }

    private void Update() {
        time += Time.deltaTime;

        if (time >= 1 && !GameOver && EnemySpawner.IsSpawning) {
            time = 0.0f;

            AddToScore(1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && GameOver) {
            GameObject.Find("Player").GetComponent<Player>().ResetPlayer();

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy")) {
                Destroy(g);
            }

            GameOver = false;
            HideGameOver();

            PlayerScore = 0;
            UpdateScore();

            EnemySpawner.ResetSpawner();
        }
    }

    public void UpdateScore() {
        Score.text = "Score: " + PlayerScore;
    }

    public void NewGame() {
        StartSpawning();
    }

    public void EndGame() {
        ShowGameOver();
        GameOver = true;
    }


    private void ShowGameOver() {
        Group.alpha = 1;
        Group.blocksRaycasts = true;
    }

    private void HideGameOver() {
        Group.alpha = 0;
        Group.blocksRaycasts = false;
    }

    // Static methods

    public static Director GetDirector() {
        return singleton;
    }

    public static void AddToScore(int scoreToAdd) {
        singleton.PlayerScore += scoreToAdd;
        singleton.UpdateScore();
    }

    public static void PlayerDied() {
        singleton.EndGame();
    }

    public static bool IsGameOver() {
        return singleton.GameOver;
    }

    public static void StartSpawning() {
        singleton.EnemySpawner.IsSpawning = true;
    }
}

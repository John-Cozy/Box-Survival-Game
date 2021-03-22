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

    public GameObject RoundText;

    private int round = 1;

    private void Start() {
        singleton = this;
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space) && GameOver) {
            GameObject.Find("Player").GetComponent<Player>().ResetPlayer();

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy")) {
                Destroy(g);
            }

            GameOver = false;
            HideGameOver();

            PlayerScore = 0;
            UpdateScore();

            round = 1;

            EnemySpawner.ResetSpawner();
            NewRound();
        } else if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }

    public void UpdateScore() {
        Score.text = "Score: " + PlayerScore;
    }

    public void NewRound() {
        RoundText.GetComponent<Text>().text = "Round " + round;
        RoundText.GetComponent<CanvasGroup>().alpha = 1;
        RoundText.GetComponent<CanvasGroup>().blocksRaycasts = true;

        StartCoroutine(HideRoundText());
    }

    IEnumerator HideRoundText() {
        yield return new WaitForSeconds(2);

        RoundText.GetComponent<CanvasGroup>().alpha = 0;
        RoundText.GetComponent<CanvasGroup>().blocksRaycasts = false;
        EnemySpawner.NewRound();
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

    public static void RoundFinished() {
        singleton.round++;
        singleton.NewRound();
    }

    public static void NewGame() {
        singleton.NewRound();
    }
}

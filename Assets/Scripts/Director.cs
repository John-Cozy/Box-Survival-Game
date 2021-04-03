using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{

    public Spawner EnemySpawner;
    public Tutorial Tutorial;

    public GameObject RoundText;

    public Text Score;
    public CanvasGroup Group;
    public int PlayerScore = 0;
    public bool GameOver = false;


    private static Director Singleton;

    private int round = 1;

    private void Start() {
        Singleton = this;
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space) && GameOver) {
            GameObject.Find("Player").GetComponent<Player>().ResetPlayer();

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy")) {
                Destroy(g);
            }

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Pickup")) {
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
        RoundText.GetComponent<Text>().text = round % 5 == 0 ? "Boss Round" : ("Round " + round);
        RoundText.GetComponent<CanvasGroup>().alpha = 1;
        RoundText.GetComponent<CanvasGroup>().blocksRaycasts = true;

        StartCoroutine(HideRoundText());
    }

    IEnumerator HideRoundText() {
        yield return new WaitForSeconds(2);

        RoundText.GetComponent<CanvasGroup>().alpha = 0;
        RoundText.GetComponent<CanvasGroup>().blocksRaycasts = false;

        EnemySpawner.NewRound(round % 5 == 0);
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
        return Singleton;
    }

    public static void AddToScore(int scoreToAdd) {
        Singleton.PlayerScore += scoreToAdd;
        Singleton.UpdateScore();
    }

    public static void PlayerDied() {
        Singleton.EndGame();
    }

    public static bool IsGameOver() {
        return Singleton.GameOver;
    }

    public static void RoundFinished() {
        Singleton.round++;
        Singleton.NewRound();
    }

    public static void NewGame() {
        Singleton.NewRound();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour {

    public GameObject PlayerPrefab;
    public GameObject RoundText;

    public Text Score;

    public UIGroup GameOverScreen;

    public int PlayerScore = 0;
    public bool GameOver = false;

    private int round = 1;

    private static Director Singleton;

    private void Start() {
        Singleton = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && GameOver) {
            Player.GetPlayer().ResetPlayer();

            DeleteEnemiesAndPickups();

            GameOver = false;
            GameOverScreen.Hide();

            PlayerScore = 0;
            UpdateScore();

            round = 1;

            Spawner.ResetSpawner();
            NewRoundInterlude();
        }
    }

    public void UpdateScore() {
        Score.text = "Score: " + PlayerScore;
    }

    public void NewRoundInterlude() {
        RoundText.GetComponent<Text>().text = round % 5 == 0 ? "Boss Round" : ("Round " + round);
        RoundText.GetComponent<UIGroup>().Show();

        StartCoroutine(NewRound());
    }

    IEnumerator NewRound() {
        yield return new WaitForSeconds(2);
        RoundText.GetComponent<UIGroup>().Hide();

        bool bossRound = round % 5 == 0;
        Spawner.NewRound(bossRound);
    }

    private void EndGame() {
        GameOverScreen.Show();
        GameOver = true;
    }

    private void SpawnPlayer() {
        Instantiate(PlayerPrefab);
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
        Singleton.NewRoundInterlude();
    }

    public static void DeleteEnemiesAndPickups() {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy")) Destroy(g);
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Pickup")) Destroy(g);
    }

    public static void ReturnToMenu() {
        DeleteEnemiesAndPickups();

        MainMenu.ShowMenu();
        Spawner.StopSpawning();
        Destroy(Player.GetPlayer().gameObject);
    }

    public static void NewGame(int Difficulty) {
        float DifficultyModifier = 1;

        switch (Difficulty) {
            case 0: DifficultyModifier = 1.0f; break;
            case 1: DifficultyModifier = 2.0f; break;
            case 2: DifficultyModifier = 3.0f; break;
        }

        Spawner.SetDifficultyModifier(DifficultyModifier);
        Spawner.ResetSpawner();

        Singleton.SpawnPlayer();
        Singleton.NewRoundInterlude();
    }
}

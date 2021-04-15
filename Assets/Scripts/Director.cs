using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class Director : MonoBehaviour {

    public GameObject PlayerPrefab;
    public GameObject Round;
    public GameObject Score;
    public GameObject Statistics;

    public UIGroup GameOverScreen;

    public int PlayerScore = 0;

    private bool GameOver = false;
    private bool GameScreenVisible = false;
    private bool StatisticsVisible = false;
    
    private float FPS;
    private float FPSReload;
    private float FPSReloadIncrement = 0.25f;
    private int round = 1;

    private static Director Singleton;

    private void Start() {
        Singleton = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && GameOver) {
            ResetGame();
        } else if (Input.GetKeyDown(KeyCode.K) && GameScreenVisible) {
            ToggleStatistic();
        }

        if (StatisticsVisible) UpdateStatistics();
    }

    private void UpdateStatistics() {
        UpdateFPS();

        Statistics.GetComponent<Text>().text = "FPS:  " + FPS +
            "\nMemory   Usage:   " + (int)(Profiler.GetTotalAllocatedMemoryLong() / 1e+6) + "MB" +
            "\nEnemy   Number:   " + GameObject.FindGameObjectsWithTag("Enemy").Length +
            "\nPlayer   Health:  " + Player.GetPlayer().GetComponent<Player>().Health + "/" + Player.GetPlayer().GetComponent<Player>().MaxHealth +
            "\nGodMode:   " + (Player.GetPlayer().GetComponent<Player>().godMode ? "Enabled" : "Disabled");
    }

    private void UpdateFPS() {
        FPSReload += Time.deltaTime;

        if (FPSReload > FPSReloadIncrement) {
            FPS = 1f / Time.deltaTime;
            FPSReload = 0;
        }
    }

    private void ToggleStatistic() {
        if (StatisticsVisible) {
            Statistics.GetComponent<UIGroup>().Hide();
            StatisticsVisible = false;
        } else {
            Statistics.GetComponent<UIGroup>().Show();
            StatisticsVisible = true;
        }
    }

    private void ResetGame() {
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

    public void UpdateScore() {
        Score.GetComponent<Text>().text = "Score: " + PlayerScore;
    }

    public void NewRoundInterlude() {
        Round.GetComponent<Text>().text = round % 5 == 0 ? "Boss Round" : ("Round " + round);
        Round.GetComponent<UIGroup>().Show();

        StartCoroutine(NewRound());
    }

    IEnumerator NewRound() {
        yield return new WaitForSeconds(2);
        Round.GetComponent<UIGroup>().Hide();

        bool bossRound = round % 5 == 0;
        if (GameScreenVisible) Spawner.NewRound(bossRound);
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

        Singleton.StatisticsVisible = false;
        Singleton.GameScreenVisible = false;
        Singleton.Statistics.GetComponent<UIGroup>().Hide();
        Singleton.Score.GetComponent<UIGroup>().Hide();
        Singleton.GameOverScreen.Hide();

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

        Singleton.round = 1;
        Singleton.Score.GetComponent<UIGroup>().Show();

        Singleton.SpawnPlayer();
        Singleton.NewRoundInterlude();

        Singleton.GameScreenVisible = true;
    }
}

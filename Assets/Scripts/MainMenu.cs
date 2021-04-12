using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIGroup {

    public Text DifficultyText;
    private int Difficulty = 1;

    private static MainMenu Singleton;

    private void Start() {
        Singleton = this;
    }

    public void StartGame() {
        Hide();
        Director.NewGame(Difficulty);
    }

    public void StartTutorial() {
        Hide();
        Tutorial.BeginTutorial();
    }

    public void CycleDifficulty() {
        switch (Difficulty) {
            case 0:
                Difficulty++;
                DifficultyText.color = new Color(0.6603774f, 0.4891209f, 0);
                DifficultyText.text = "NORMAL";
                break;
            case 1:
                Difficulty++;
                DifficultyText.color = new Color(0.6588235f, 0.03190858f, 0);
                DifficultyText.text = "HARD";
                break;
            case 2:
                Difficulty = 0;
                DifficultyText.color = new Color(0, 0.6588235f, 0.03508177f);
                DifficultyText.text = "EASY";
                break;
        }
    }

    public static void ShowMenu() {
        Singleton.Show();
    }
}

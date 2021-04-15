using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIGroup {

    public Text DifficultyText;
    public Slider Volume;
    private int Difficulty = 1;

    private static MainMenu Singleton;

    private void Start() {
        Singleton = this;
    }

    public void StartGame() {
        AudioManager.SetSounds(Volume.value);
        AudioManager.Play("Menu");

        Hide();
        Director.NewGame(Difficulty);
    }

    public void StartTutorial() {
        AudioManager.SetSounds(Volume.value);
        AudioManager.Play("Menu");
        Hide();
        Tutorial.BeginTutorial();
    }

    public void CycleDifficulty() {
        AudioManager.SetSounds(Volume.value);

        switch (Difficulty) {
            case 0:
                Difficulty++;
                DifficultyText.color = new Color(0.6603774f, 0.4891209f, 0);
                DifficultyText.text = "NORMAL";
                AudioManager.Play("Normal");
                break;
            case 1:
                Difficulty++;
                DifficultyText.color = new Color(0.6588235f, 0.03190858f, 0);
                DifficultyText.text = "HARD";
                AudioManager.Play("Hard");
                break;
            case 2:
                Difficulty = 0;
                DifficultyText.color = new Color(0, 0.6588235f, 0.03508177f);
                DifficultyText.text = "EASY";
                AudioManager.Play("Easy");
                break;
        }
    }

    public static void ShowMenu() {
        Singleton.Show();
    }
}

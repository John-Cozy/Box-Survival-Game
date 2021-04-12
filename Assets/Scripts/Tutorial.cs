using UnityEngine;
using UnityEngine.UI;

public class Tutorial : UIGroup {

    public GameObject PlayerPrefab;
    public GameObject PickupPostPrefab;

    public Text W;
    public Text A;
    public Text S;
    public Text D;
    public Text G;
    public Text ESC;
    public Text LeftClick;
    
    public UIGroup ShootPrompt;
    public UIGroup PickupPrompt;

    private bool TutorialEnabled = false;
    private bool g1, g2;

    private GameObject PickupPost;
    private GameObject Player;

    private static Tutorial Singleton;

    private void Start() {
        Singleton = this;
    }

    void Update() {
        if (TutorialEnabled) {
            if (Input.GetMouseButtonDown(0)) ChangeColour(LeftClick, Color.green);
            if (Input.GetKeyDown(KeyCode.W)) ChangeColour(W, Color.green);
            if (Input.GetKeyDown(KeyCode.A)) ChangeColour(A, Color.green);
            if (Input.GetKeyDown(KeyCode.S)) ChangeColour(S, Color.green);
            if (Input.GetKeyDown(KeyCode.D)) ChangeColour(D, Color.green);
            if (Input.GetKeyDown(KeyCode.G)) {
                if (!g1) {
                    ChangeColour(G, Color.red);
                    g1 = true;
                } else if (g1 && !g2) {
                    ChangeColour(G, Color.green);
                    g2 = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                ChangeColour(ESC, Color.green);
                EndTutorial(); 
            }
        }
    }

    private void StartTutorial() {
        Show();
        ShootPrompt.Show();

        PickupPost = Instantiate(PickupPostPrefab, new Vector2(7.59f, -3.95f), Quaternion.identity);
        Player     = Instantiate(PlayerPrefab);

        TutorialEnabled = true;
    }

    private void EndTutorial() {
        Hide();
        MainMenu.ShowMenu();
        PickupPrompt.Hide();

        PickupPost.GetComponent<PickupPost>().SelfDestruct();
        Destroy(Player);

        TutorialEnabled = false;
    }

    private void ChangeColour(Text text, Color colour) {
        text.color = colour;
    }

    public static void BeginTutorial() {
        Singleton.StartTutorial();
    }

    public static void FinishTutorial() {
        Singleton.EndTutorial();
    }

    public static void ChangePrompt() {
        Singleton.ShootPrompt.Hide();
        Singleton.PickupPrompt.Show();
    }
}

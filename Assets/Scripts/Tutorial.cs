using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public CanvasGroup Group;
    public Text W;
    public Text A;
    public Text S;
    public Text D;
    public Text Click;

    private bool wClicked, aClicked, sClicked, dClicked, clickClicked;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            ChangeColour(W);
            wClicked = true;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            ChangeColour(A);
            aClicked = true;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            ChangeColour(S);
            sClicked = true;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            ChangeColour(D);
            dClicked = true;
        } else if (Input.GetMouseButtonDown(0)) {
            ChangeColour(Click);
            clickClicked = true;
        }

        if ((wClicked && aClicked && sClicked && dClicked && clickClicked)) {
            Group.alpha -= 0.002f;

            if (Group.alpha == 1) {
                HideTutorial();
            } else if (Group.alpha <= 0) {
                Director.StartSpawning();
                Destroy(gameObject);
            }
        }
    }

    private void ChangeColour(Text text) {
        text.color = Color.green;
    }

    private void HideTutorial() {
        Group.blocksRaycasts = false;
    }
}

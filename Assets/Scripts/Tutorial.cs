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
    public Text G;
    public Text LeftClick;

    private bool w, a, s, d, lClick, g;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            ChangeColour(W);
            w = true;
        } 
        if (Input.GetKeyDown(KeyCode.A)) {
            ChangeColour(A);
            a = true;
        } 
        if (Input.GetKeyDown(KeyCode.S)) {
            ChangeColour(S);
            s = true;
        } 
        if (Input.GetKeyDown(KeyCode.D)) {
            ChangeColour(D);
            d = true;
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            ChangeColour(G);
            g = true;
        }
        if (Input.GetMouseButtonDown(0)) {
            ChangeColour(LeftClick);
            lClick = true;
        }
    }

    private void FixedUpdate() {
        if (w && a && s && d && lClick && g) {
            Group.alpha -= 0.05f;

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

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

    private bool w, a, s, d, lClick, g1, g2;
    private bool startedGame;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            ChangeColour(W, Color.green);
            w = true;
        } 
        if (Input.GetKeyDown(KeyCode.A)) {
            ChangeColour(A, Color.green);
            a = true;
        } 
        if (Input.GetKeyDown(KeyCode.S)) {
            ChangeColour(S, Color.green);
            s = true;
        } 
        if (Input.GetKeyDown(KeyCode.D)) {
            ChangeColour(D, Color.green);
            d = true;
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            if (!g1) {
                ChangeColour(G, Color.red);
                g1 = true;
            } else if (g1 && !g2) {
                ChangeColour(G, Color.green);
                g2 = true;
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            ChangeColour(LeftClick, Color.green);
            lClick = true;
        }
    }

    private void FixedUpdate() {
        if (w && a && s && d && lClick && g2 && !startedGame) {
            Group.alpha -= 0.05f;

            if (Group.alpha == 1) {
                Group.blocksRaycasts = false;
            } else if (Group.alpha <= 0) {
                Director.NewGame();
                startedGame = true;
            }
        }
    }

    private void ChangeColour(Text text, Color colour) {
        text.color = colour;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public SpriteRenderer SpriteRenderer;

    public float MoveSpeed;
    public int MaxHealth;

    public Color InitialColour, DeadColour;

    public string HitAudio, DeadAudio;

    protected int health;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }

    protected void UpdateColour() {
        SpriteRenderer.color = Color.Lerp(DeadColour, InitialColour, (float) health / MaxHealth);
    }
}

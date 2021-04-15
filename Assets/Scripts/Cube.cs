using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public SpriteRenderer SpriteRenderer;

    public float MoveSpeed;
    public int MaxHealth;

    public Color InitialColour, DeadColour;

    public string HitAudio, DeadAudio;

    public int Health;

    protected void Start()
    {
        Health = MaxHealth;
    }

    protected void UpdateColour() {
        SpriteRenderer.color = Color.Lerp(DeadColour, InitialColour, (float) Health / MaxHealth);
    }
}

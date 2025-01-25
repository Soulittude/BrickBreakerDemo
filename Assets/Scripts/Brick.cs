using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer sr { get; private set;}
    public Sprite[] states;

    public int brickHealth { get; private set; }
    public bool breakable;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (breakable)
        {
            brickHealth = states.Length;
            sr.sprite = states[brickHealth - 1];
        }
    }

    private void Hit()
    {
        if(!breakable)
            return;
        
        brickHealth--;

        if(brickHealth<=0)
            gameObject.SetActive(false);
        else
            sr.sprite = states[brickHealth - 1];
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Ball")
            Hit();
    }
}

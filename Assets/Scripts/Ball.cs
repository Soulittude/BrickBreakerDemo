using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Vector2 dir {get; private set;}

    public float ballSpeed = 400f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1;

        rb.AddForce(force * ballSpeed);
    }
}

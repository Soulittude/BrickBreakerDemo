using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    public float ballSpeed = 500f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;

        //Function starts after 1 second delay
        Invoke(nameof(SetRandomLaunch), 1f);
    }

    private void SetRandomLaunch()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1;

        rb.AddForce(force.normalized * ballSpeed);
    }
}

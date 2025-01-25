using Unity.VisualScripting;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Vector2 dir { get; private set; }
    public float speed = 30f;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            this.dir = Vector2.left;

        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            this.dir = Vector2.right;

        else
            this.dir = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if(this.dir != Vector2.zero)
            this.rb.AddForce(this.dir * this.speed);
    }
}

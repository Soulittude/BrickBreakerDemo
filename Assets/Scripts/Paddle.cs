
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Vector2 dir { get; private set; }
    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddle()
    {
        transform.position = new Vector2(0f, transform.position.y);
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            dir = Vector2.left;

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;

        else
            dir = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (dir != Vector2.zero)
            rb.AddForce(dir * speed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = transform.position;
            Vector2 contactPoint = col.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = col.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);
            float bounceAngle = (offset / width) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
        
            Quaternion rot = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rb.velocity = rot * Vector2.up * ball.rb.velocity.magnitude;
        }
    }
}

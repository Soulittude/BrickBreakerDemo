
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Vector2 dir { get; private set; }
    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            this.dir = Vector2.left;

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            this.dir = Vector2.right;

        else
            this.dir = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (this.dir != Vector2.zero)
            this.rb.AddForce(this.dir * this.speed);
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

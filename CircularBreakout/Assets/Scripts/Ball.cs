using UnityEngine;
using System.Collections;

/// <summary>
/// A bouncy ball.
/// </summary>
public class Ball : MonoBehaviour
{
    [Tooltip("The initial speed of the ball, measured in world units per second.")]
    public float InitialSpeed;

    [Tooltip("The sound to play when the ball bounces.")]
    public AudioClip BounceSound;

    [Tooltip("The sound to play when the ball goes off-screen.")]
    public AudioClip DeathSound;

    [Tooltip("The sound to play when the ball resets.")]
    public AudioClip ResetSound;

    private Rigidbody2D body;
    private new Collider2D collider;
    private new AudioSource audio;
    private new SpriteRenderer renderer;
    private Paddle paddle;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if (body == null) throw new MissingComponentException("Expected attached Rigidbody2D");

        collider = GetComponent<Collider2D>();
        if (collider == null) throw new MissingComponentException("Expected attached Collider2D");

        audio = GetComponent<AudioSource>();
        if (audio == null) throw new MissingComponentException("Expected attached AudioSource");

        renderer = GetComponent<SpriteRenderer>();
        if (renderer == null) throw new MissingComponentException("Expected attached SpriteRenderer");

        paddle = GameObject.FindGameObjectWithTag("Player").GetComponent<Paddle>();
        if (paddle == null) throw new MissingReferenceException("Expected Paddle tagged with 'Player' in scene.");

        Invoke("Reset", 0.5f);  //give the paddle a sec to get a position
    }

    public void OnBecameInvisible()
    {
        audio.pitch = 1.0f;
        PlaySound(DeathSound);
        Invoke("Reset", 1.5f);
    }

    private void Reset()
    {
        renderer.enabled = true;
        collider.enabled = true;
        body.position = GetSpawnPoint();
        body.velocity = GetSpawnVelocity();
        PlaySound(ResetSound);
    }

    private Vector2 GetSpawnPoint()
    {
        //calculate the point a little closer in from the paddle's position
        return Common.GetPointOnCircle(paddle.CurrentAngle, paddle.BoundsRadius - renderer.bounds.size.y);
    }

    private Vector2 GetSpawnVelocity()
    {
        //point the ball away from the paddle
        return Vector2.ClampMagnitude(paddle.transform.position - transform.position, InitialSpeed);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        PlaySound(BounceSound);

        if (collision.collider.CompareTag("Player"))
        {
            audio.pitch = 1f;
        }
        else if(collision.collider.CompareTag("Block"))
        {
            audio.pitch += 0.1f;
            body.velocity *= 1.1f;
            Destroy(collision.collider.gameObject);

            //TODO: check if we just destroyed the last block, say you win, load next level, etc
        }
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound == null) return;
        audio.clip = sound;
        audio.Play();
    }
}

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
    private new AudioSource audio;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if (body == null) throw new MissingComponentException("Expected: Rigidbody2D");

        audio = GetComponent<AudioSource>();
        if (audio == null) throw new MissingComponentException("Expected: AudioSource");

        Reset();
    }

    public void OnBecameInvisible()
    {
        audio.pitch = 1.0f;
        PlaySound(DeathSound);
        Invoke("Reset", 1.5f);
    }

    private void Reset()
    {
        //TODO: start the ball at the paddle instead of the origin
        body.position = Vector2.zero;
        body.velocity = GetRandomVelocity();
        PlaySound(ResetSound);
    }

    private Vector2 GetRandomVelocity()
    {
        float angle = Common.GetRandomAngle();
        return Common.GetPointOnCircle(angle, InitialSpeed);
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
        }
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound == null) return;
        audio.clip = sound;
        audio.Play();
    }
}

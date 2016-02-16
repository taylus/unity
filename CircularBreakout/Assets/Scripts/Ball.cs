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

    private Rigidbody2D body;
    private new Collider2D collider;
    private new AudioSource audio;
    private new SpriteRenderer renderer;
    private Paddle paddle;
    private LevelLoader levelLoader;

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

        levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        if (levelLoader == null) throw new MissingReferenceException("Expected object of type LevelLoader in scene.");

        Invoke("Reset", 0.5f);  //give the paddle a sec to get a position
    }

    public void OnBecameInvisible()
    {
        //ignore when we purposely make the ball invisible
        if (!renderer.enabled) return;

        //run this when the ball goes offscreen
        audio.pitch = 1.0f;
        PlaySound(DeathSound);
        Invoke("Reset", 1.5f);
    }

    private void Reset()
    {
        audio.pitch = 1.0f;
        renderer.enabled = true;
        collider.enabled = true;
        transform.position = GetSpawnPoint();
        body.velocity = GetSpawnVelocity();
        PlaySound(BounceSound);
    }

    private void Disable()
    {
        renderer.enabled = false;
        collider.enabled = false;
        body.velocity = Vector2.zero;
    }

    private Vector2 GetSpawnPoint()
    {
        //calculate the point a little closer in from the paddle's position
        return Common.GetPointOnCircle(paddle.CurrentAngle, paddle.BoundsRadius - renderer.bounds.size.y);
    }

    private Vector2 GetSpawnVelocity()
    {
        //point the ball towards the center of the screen
        return (-body.position).normalized * InitialSpeed;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        PlaySound(BounceSound);

        if (collision.collider.CompareTag("Player"))
        {
            audio.pitch = 1f;
        }
        else if (collision.collider.CompareTag("Block"))
        {
            audio.pitch += 0.1f;
            body.velocity *= 1.1f;
            collision.collider.enabled = false;

            //hide the ball immediately when we hit the last block
            if (levelLoader.BlockCount == 1) Disable();

            StartCoroutine("FadeOutAndDestroy", collision.collider.gameObject);
        }
    }

    private IEnumerator FadeOutAndDestroy(GameObject block)
    {
        Renderer r = block.GetComponent<Renderer>();
        for (float alpha = 1.0f; alpha >= 0; alpha -= 0.1f)
        {
            Color color = r.material.color;
            color.a = alpha;
            r.material.color = color;
            block.transform.localScale *= 1.125f;
            yield return new WaitForSeconds(0.01f);
        }

        levelLoader.DestroyBlock(block);
        if (levelLoader.GameOver())
        {
            //TODO: something exciting because they won
            Invoke("LoadNextLevel", 1.5f);
        }
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound == null) return;
        audio.clip = sound;
        audio.Play();
    }

    private void LoadNextLevel()
    {
        levelLoader.Load();
        Reset();
    }
}

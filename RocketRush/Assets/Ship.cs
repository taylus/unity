using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D body;
    private ParticleSystem smoke;
    private Renderer fire; //the fire sprite to display when thrusting

    public float Thrust;
    public float Handling; //higher number -> faster turns

    //TODO: add clouds, birds? need some reference points in the sky
    //TODO: add altimeter
    //TODO: screen shake when thrusting
    //TODO: replacable/upgradable ship parts that affect stats

    //other stats?
    //- top speed
    //- weight
    //- fuel capacity
    //- fuel efficiency
    //- hull (hp)
    //- shields?

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();

        Transform fire = transform.Find("Fire");
        if (fire != null) this.fire = fire.GetComponent<Renderer>();

        Transform smoke = transform.Find("Smoke");
        if (smoke != null) this.smoke = smoke.GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        body.rotation += Input.GetAxis("Horizontal") * -Handling;

        float vertical = Input.GetAxis("Vertical");
        body.AddForce(transform.up * vertical * Thrust);

        if (fire != null) fire.enabled = vertical > 0;
        if (smoke != null) smoke.enableEmission = vertical > 0;
    }
}

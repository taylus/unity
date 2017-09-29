﻿using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D body;
    private ParticleSystem smoke;
    private Renderer fire; //the fire sprite to display when thrusting

    public CameraShake shake;
    public float Thrust;
    public float Handling; //higher number -> faster turns

    //TODO: add clouds, birds? need some reference points in the sky
    //TODO: add altimeter
    //TODO: replacable/upgradable ship parts that affect stats
    //TODO: make gravity weaker as the ship's altitude increases? (making it hard to take off)

    //other stats?
    //- top speed
    //- weight
    //- fuel capacity
    //- fuel efficiency
    //- hull (hp)
    //- shields?

    private const float ShakeCeiling = 10f;     //the height at which the screen stops shaking from ship thrust

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        Transform fire = transform.Find("Fire");
        if (fire != null) this.fire = fire.GetComponent<Renderer>();

        Transform smoke = transform.Find("Smoke");
        if (smoke != null) this.smoke = smoke.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        body.rotation += Input.GetAxis("Horizontal") * -Handling;

        float vertical = Input.GetAxis("Vertical");
        body.AddForce(transform.up * vertical * Thrust);

        if (fire != null) fire.enabled = vertical > 0;
        if (smoke != null) smoke.enableEmission = vertical > 0;

        //TODO: make this continuous?
        if (transform.position.y <= ShakeCeiling)
            shake.shakeDuration = vertical;
        else
            shake.shakeDuration = 0;
    }
}

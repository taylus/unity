using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D body;
    public float Thrust;
    public float Handling; //higher number -> faster turns

    //TODO: add clouds, birds? need some reference points in the sky
    //TODO: add altimeter
    //TODO: make fire appear when thrusting
    //TODO: screen shake when thrusting
    //TODO: particle/smoke effects when thrusting
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
    }

    public void Update()
    {
        body.rotation += Input.GetAxis("Horizontal") * -Handling;
        body.AddForce(transform.up * Input.GetAxis("Vertical") * Thrust);
    }
}

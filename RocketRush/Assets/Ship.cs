using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    private Rigidbody2D body;
    private ParticleSystem smoke;
    private Renderer fire; //the fire sprite to display when thrusting
    private float currentFuel = 100; //% of fuel remaining

    public Text Altimeter;
    public Text Speedometer;
    public Text FuelGauge;
    public CameraShake Shake;
    public float Thrust;
    public float Handling; //higher number -> faster turns
    public float FuelEfficiency; //higher number -> less fuel used per frame of thrust

    //TODO: add clouds, birds? need some reference points in the sky
    //TODO: parallax mountains in the background?
    //TODO: replacable/upgradable ship parts that affect stats
    //TODO: make gravity weaker as the ship's altitude increases? (making it harder/more dramatic to take off)

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

        if (FuelEfficiency <= 0) FuelEfficiency = 1;
    }

    private void Update()
    {
        Altimeter.text = string.Format("Altitude: {0:f1}", transform.position.y);
        Speedometer.text = string.Format("Speed: {0:f1}", body.velocity.y);
        UpdateFuelGauge(FuelGauge, currentFuel);
    }

    private void FixedUpdate()
    {
        if (currentFuel > 0)
        {
            body.rotation += Input.GetAxis("Horizontal") * -Handling;
            float vertical = Input.GetAxis("Vertical");
            body.AddForce(transform.up * vertical * Thrust);
            if (fire != null) fire.enabled = vertical > 0;
            if (smoke != null) EnableSmoke(vertical > 0);
            currentFuel -= vertical / FuelEfficiency;

            //TODO: make this continuous?
            if (transform.position.y <= ShakeCeiling)
                Shake.shakeDuration = vertical;
            else
                Shake.shakeDuration = 0;
        }
        else
        {
            if (fire != null) fire.enabled = false;
            if (smoke != null) EnableSmoke(false);
            Shake.shakeDuration = 0;
        }
    }

    private void EnableSmoke(bool enabled)
    {
        var emission = smoke.emission;
        emission.enabled = enabled;
    }

    private static void UpdateFuelGauge(Text fuelGaugeText, float fuelPercentage)
    {
        var color = GetFuelGaugeColor(fuelPercentage);
        fuelGaugeText.text = string.Format("Fuel: <color=#{1:X2}{2:X2}{3:X2}>{0:f0}%</color>", fuelPercentage, color.r, color.g, color.b);
    }

    private static Color32 GetFuelGaugeColor(float fuelPercentage)
    {
        if (fuelPercentage < 20) return Color.red;
        if (fuelPercentage < 50) return Color.yellow;
        return Color.green;
    }

    public void AddFuel(float fuelAmount)
    {
        currentFuel += fuelAmount;
        if (currentFuel > 100) currentFuel = 100;
    }
}

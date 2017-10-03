using UnityEngine;

/// <summary>
/// A fuel canister that the player can pick up for extra rocket fuel.
/// </summary>
public class Fuel : MonoBehaviour
{
    public float FuelAmount;

    private void Update()
    {
        Oscillate(30, 3);
    }

    /// <summary>
    /// Oscillate rotation between -degrees and degrees.
    /// </summary>
    private void Oscillate(float degrees, float speed)
    {
        var rot = transform.eulerAngles;
        rot.z = Mathf.Sin(Time.time * speed) * degrees;
        transform.eulerAngles = rot;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //give the ship some fuel
        var ship = other.GetComponent<Ship>();
        if (ship != null) ship.AddFuel(FuelAmount);

        //TODO: vfx/sfx for getting the collectible

        Destroy(gameObject);
    }
}

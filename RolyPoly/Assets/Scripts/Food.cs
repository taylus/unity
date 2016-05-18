using UnityEngine;

public class Food : MonoBehaviour
{
    [Tooltip("What is this food called?")]
    public string Name;

    [Tooltip("How fat will Roly get from eating this?")]
    public float Calories;

    public bool IsTasty { get { return Calories > 0; } }

    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

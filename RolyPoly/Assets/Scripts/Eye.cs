using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour
{
    public bool TrackCursor = true;
    public GameObject Pupil;

    public void Update()
    {
        if (TrackCursor && Pupil != null)
        {
            Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LookAt(mouseWorldPoint);
        }
    }

    private void LookAt(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, transform.position - position, Mathf.Infinity, 1 << gameObject.layer);
        Debug.DrawLine(transform.position, position, Color.green);
        if(hit.collider != null)
        {
            Pupil.transform.position = hit.point;
        }
        else
        {
            Pupil.transform.position = position;
        }
    }
}

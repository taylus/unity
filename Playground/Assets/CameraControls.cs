using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float MoveSpeed = 0.1f;
    public float MouseSensitivity = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public void Update()
    {
        //move
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        float yAxisValue = Input.GetAxis("Jump");
        transform.Translate(new Vector3(xAxisValue * MoveSpeed, yAxisValue * MoveSpeed, zAxisValue * MoveSpeed));

        //look
        yaw += MouseSensitivity * Input.GetAxis("Mouse X");
        pitch -= MouseSensitivity * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKey(KeyCode.Escape)) Quit();
    }

    private void Quit()
    {
        if(Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}

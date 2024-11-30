using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(player.position.x, player.position.y + 4.0f, player.position.z + 10.0f);
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;

        transform.LookAt(player.position);

        transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * turnSpeed);
    }
}

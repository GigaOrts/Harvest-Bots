using UnityEngine;

public class CameraFacing : MonoBehaviour
{
    private void Start()
    {
        transform.forward = Camera.main.transform.forward;
    }
}

using UnityEngine;

public class BillboardObject : MonoBehaviour
{
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    protected void Billboard()
    {
        if (mainCam != null)
        {
            Vector3 dir = (transform.position - mainCam.transform.position).normalized;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}

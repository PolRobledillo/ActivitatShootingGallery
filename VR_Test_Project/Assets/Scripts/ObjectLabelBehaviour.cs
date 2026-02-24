using UnityEngine;

public class ObjectLabelBehaviour : MonoBehaviour
{
    public GameObject camera;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
    }
}

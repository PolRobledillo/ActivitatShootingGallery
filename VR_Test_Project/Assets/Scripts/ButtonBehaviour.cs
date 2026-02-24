using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public Vector3 position;
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.ResetScore();
    }
}

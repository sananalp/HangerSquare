using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void FixedUpdate()
    {
        Vector3 pos = player.transform.position;
        gameObject.transform.position = new Vector3(pos.x, 0f, -10f);
    }
}

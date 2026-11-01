using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;
    public float spinSpeed = 90f;
    public float bobHeight = 0.2f;
    public float bobSpeed = 2f;

    Vector3 startPos;

    void Start() => startPos = transform.position;

    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
        transform.position = startPos +
            Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            GameManager.Instance.AddCoin(value);
            Destroy(gameObject);
        }
    }
}
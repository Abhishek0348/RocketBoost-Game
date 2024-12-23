using UnityEngine;

public class Ossilatory : MonoBehaviour
{
    [SerializeField] Vector3 MovementVector;
    float movementFactor;
    [SerializeField] float speed;
    Vector3 startPosition;
    Vector3 endPosition;
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + MovementVector;
    }
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}

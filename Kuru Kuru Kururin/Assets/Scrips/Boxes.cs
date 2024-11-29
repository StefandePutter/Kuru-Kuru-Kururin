using UnityEngine;

public class Boxes : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float totalDistance;
    [Tooltip("If the box has to go up.")]
    [SerializeField] private bool directionUp;

    // Update is called once per frame
    void Update()
    {
        // get y pos based on PingPong time * total distance - half
        float y = Mathf.PingPong(Time.time * speed, 1) * totalDistance - totalDistance/2;
        if (!directionUp)
        {
            y *= -1;
        }
        gameObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}

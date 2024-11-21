using System.Collections.Specialized;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private InputManager input;
       
    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();

        GameObject inp = GameObject.Find("InputManager");
        input = inp.GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
        m_Rigidbody.rotation += -rotateSpeed;

        transform.Translate(input.moveValue * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //transform.position = Vector3.zero;

        Vector2 bounceDirection = collision.contacts[0].normal;
        m_Rigidbody.AddForce(bounceDirection * 5f, ForceMode2D.Impulse);
        Invoke("StopForce", 0.5f);

        Debug.Log("we hit something");
    }

    private void StopForce()
    {
        m_Rigidbody.linearVelocity = Vector2.zero;
    }
}

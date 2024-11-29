using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer flag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject tmp = transform.Find("Flag").gameObject;
        flag = tmp.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            flag.color = Color.green;
        }
    }
}

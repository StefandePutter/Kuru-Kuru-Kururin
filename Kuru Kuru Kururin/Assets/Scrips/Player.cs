using System.Collections.Specialized;
using UnityEngine;

public enum PlayerState
{
    playing = 0,
    pauzed
}

public class Player : MonoBehaviour
{
    public bool hasDied = false;
    public bool hasWon = false;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int health;

    public PlayerState state;

    [SerializeField] private GameObject[] hearts = new GameObject[3];
    [SerializeField] private Transform[] turrets = new Transform[26];

    private float cooldownWindow = 2f;
    private float cooldownTime;


    private InputManager input;

    private ProjectileSpawner projectileSpawner;
       
    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();

        GameObject inp = GameObject.Find("InputManager");
        input = inp.GetComponent<InputManager>();

        projectileSpawner = GetComponent<ProjectileSpawner>();

        int i = 0;
        foreach (Transform child in transform)
        {
            foreach (Transform turret in child)
            {
                if (turret.name == "Square")
                {
                    turrets[i] = turret;
                    i++;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == PlayerState.pauzed)
        {
            return;
        }

        m_Rigidbody.rotation += rotateSpeed;

        transform.Translate(moveSpeed * Time.deltaTime * input.moveValue, Space.World);

        // if player is using special and cooldown is over
        if (input.isUsingSpecial && Time.time > cooldownTime)
        {
            Special();

            cooldownTime = Time.time + cooldownWindow;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Finish")
        {
            Debug.Log("Hit finish");   
            hasWon = true;
            state = PlayerState.pauzed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Projectile")
        {
            return;
        }


        health -= 1;
        hearts[health].SetActive(false);

        transform.position = new Vector3(-3,0,0);

        if (health == 0)
        {
            // end game
            hasDied = true;
            state = PlayerState.pauzed;
        }

        ContactPoint2D cp = collision.GetContact(0);
        Vector2 dir = cp.normal;


        Debug.Log("we hit: " + collision.gameObject.name);
    }

    private void Special()
    {
        Debug.Log("doing special attack");
        // for each turret
        foreach (Transform turret in turrets) 
        {
            // get projectile from pool
            Projectile projectile = projectileSpawner.projectilePool.Get();
            if (projectile != null)
            {
                projectile.Initialize(turret);
            }
        }
    }
}

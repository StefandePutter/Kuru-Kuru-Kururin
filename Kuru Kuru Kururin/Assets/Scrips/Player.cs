using System.Collections.Specialized;
using UnityEngine;

public enum PlayerState
{
    playing = 0,
    died,
    won
}

public class Player : MonoBehaviour
{
    // i like to put my public variables first
    public PlayerState state;
    public Vector3 respawnPoint;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int health;
    [SerializeField] private GameObject hitEffect;

    [SerializeField] private GameObject[] hearts = new GameObject[3];
    [SerializeField] private Transform[] turrets = new Transform[26];

    private float cooldownWindow = 2f;
    private float cooldownTime;
    private float hitFramesTime;

    private InputManager input;
    private ProjectileSpawner projectileSpawner;
    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        respawnPoint = transform.position;

        // get/set all objects
        m_Rigidbody = GetComponent<Rigidbody2D>();

        GameObject inp = GameObject.Find("InputManager");
        input = inp.GetComponent<InputManager>();

        projectileSpawner = GetComponent<ProjectileSpawner>();

        // get all turrets connected to player
        int i = 0;
        foreach (Transform child in transform)
        {
            // get barrel connected to the turret
            foreach (Transform turret in child)
            {
                if (turret.name == "Barrel")
                {
                    turrets[i] = turret;
                    i++;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == PlayerState.died || state == PlayerState.won)
        {
            return;
        }

        // dont move when hit exept for current force
        if (Time.time > hitFramesTime)
        {
            m_Rigidbody.rotation += rotateSpeed;
            transform.Translate(moveSpeed * Time.deltaTime * input.moveValue, Space.World);
        }

        // if player is using special and cooldown is over
        if (input.isUsingSpecial && Time.time > cooldownTime)
        {
            Special();

            cooldownTime = Time.time + cooldownWindow;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (collision.gameObject.name)
        {
            case "Finish":
                state = PlayerState.won;
                break;
            case "Checkpoint":
                respawnPoint = collision.transform.position;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Projectile" || Time.time < hitFramesTime)
        {
            return;
        }

        // remove health and hearth
        health -= 1;
        hearts[health].SetActive(false);

        //transform.position = new Vector3(-3,0,0);

        if (health == 0)
        {
            // end game
            state = PlayerState.died;
        }

        // get contact point of the wall
        ContactPoint2D cp = collision.GetContact(0);
        
        // init particle 
        Instantiate(hitEffect, cp.point, Quaternion.identity);
        
        // get direction to add force into player
        Vector2 dir = cp.normal;
        m_Rigidbody.AddForce(dir * 500);
        
        // set hitframe time for moving and no hit frames
        hitFramesTime = Time.time + 0.5f;
    }

    private void Special()
    {
        // for each turret
        foreach (Transform turret in turrets) 
        {
            // get projectile from pool
            Projectile projectile = projectileSpawner.projectilePool.Get();
            if (projectile != null)
            {
                // start projectile script with turret Transform
                projectile.Initialize(turret);
            }
        }
    }

    public void Respawn()
    {
        state = PlayerState.playing;
        health = 3;
        foreach (GameObject heart in hearts)
        {
            heart.SetActive(true);
        }
        cooldownTime = Time.time + 0.5f;
        transform.position = respawnPoint;
    }
}

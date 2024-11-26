using System.Collections.Specialized;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int health;

    private float cooldownWindow = 2f;
    private float cooldownTime;

    [SerializeField] private Transform[] turrets = new Transform[26];

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
        m_Rigidbody.rotation += rotateSpeed;

        transform.Translate(moveSpeed * Time.deltaTime * input.moveValue, Space.World);

        // if player is using special and cooldown is over
        if (input.isUsingSpecial && Time.time > cooldownTime)
        {
            Special();

            cooldownTime = Time.time + cooldownWindow;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Projectile")
        {
            return;
        }

        projectileSpawner.projectilePool.Clear();
        transform.position = Vector3.zero;

        health -= 1;
        if (health == 0)
        {
            // end game
        }

        Debug.Log("we hit: " + collision.gameObject.name);
    }

    private void Special()
    {
        Debug.Log("doing special attack");
        foreach (Transform turret in turrets) 
        {
            Projectile projectile = projectileSpawner.projectilePool.Get();
            if (projectile != null)
            {
                projectile.transform.SetPositionAndRotation(turret.position, turret.rotation);
            }
        }
    }
}

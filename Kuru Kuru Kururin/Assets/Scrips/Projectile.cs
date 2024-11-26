using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeoutDelay = 2f;

    private IObjectPool<Projectile> projectilePool;

    public IObjectPool<Projectile> ProjectilePool { set => projectilePool  = value; }

    private Rigidbody2D m_Rigidbody;

    // has to be awake or onEnable will try to use object that isnt assigned yet
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // gets called after Awake and every time on enable
    private void OnEnable()
    {
        m_Rigidbody.AddForce(transform.right * speed);

        StartCoroutine(DisableAfterSeconds(timeoutDelay));
    }

    // disable object after 2 seconds
    IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        m_Rigidbody.linearVelocity = Vector3.zero;
        m_Rigidbody.angularVelocity = 0f;

        projectilePool.Release(this);
    }
}
using System;
using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeoutDelay = 2f;

    private float randomRotation;

    private IObjectPool<Projectile> projectilePool;

    public IObjectPool<Projectile> ProjectilePool { set => projectilePool  = value; }

    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();

        randomRotation = UnityEngine.Random.Range(-10f, 10f);
    }

    // gets called after Awake and every time on enable
    public void Initialize(Transform turret)
    {
        // rotate the bullet a little to create a more chaotic pattern
        turret.Rotate(new Vector3(0,0,randomRotation));
        transform.SetPositionAndRotation(turret.position, turret.rotation);
        
        m_Rigidbody.AddForce(transform.right * speed);

        // start a coroutine which is an asynchronous function that can also
        // await part of its execution until certain conditions are met
        StartCoroutine(DisableAfterSeconds(timeoutDelay));
    }

    // coroutines are defined with an IEnumerator has to be called with StartCoroutine
    IEnumerator DisableAfterSeconds(float seconds)
    {
        // wait until time have gone by
        yield return new WaitForSeconds(seconds);

        m_Rigidbody.linearVelocity = Vector3.zero;
        m_Rigidbody.angularVelocity = 0f;

        // place projectile back in pool by calling its release function
        projectilePool.Release(this);
    }
}
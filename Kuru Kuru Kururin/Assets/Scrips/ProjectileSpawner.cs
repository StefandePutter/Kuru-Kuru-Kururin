using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;

    public IObjectPool<Projectile> projectilePool;

    private GameObject parent;

    private void Awake()
    {
        projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGetProjectileFromPool, OnReturnProjectileToPool, OnDestroyProjectile, true, 28, 56);

        // making a parent for storing the pool
        parent = new GameObject
        {
            name = "Projectiles"
        };
    }

    // what to do when creating new object for the pool
    private Projectile CreateProjectile()
    {
        Projectile projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.ProjectilePool = projectilePool;

        projectileInstance.gameObject.transform.parent = parent.transform;

        return projectileInstance;
    }

    // what to do when we get an object from pool
    private void OnGetProjectileFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    // to do when returning object to pool 
    private void OnReturnProjectileToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    // pool will always spawn objects even if above max size.
    // but it will destroy them after use instead of returning them to the pool.
    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}

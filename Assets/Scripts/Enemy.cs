using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float minDelay = 5f;
    [SerializeField] float maxDelay = 20f;
    [SerializeField] float maxDistanceProjectileAfterPassingPlayer = 50f;
    [SerializeField] float projectileSpeed = 0.2f;
    [SerializeField] float deltaProjectileSpeed = 0.001f;
    private GameObject targetToKill;

    private void Start()
    {
        targetToKill = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform);
        bullet.transform.position = transform.position;
        while (bullet.transform.position.x < targetToKill.transform.position.x + maxDistanceProjectileAfterPassingPlayer)
        {
            bullet.transform.Translate(Vector3.right * projectileSpeed);
            yield return new WaitForSeconds(deltaProjectileSpeed);
        }
        Destroy(bullet);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        StartCoroutine(Shot());
    }
}

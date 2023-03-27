using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public delegate void OnSpawned();
    public static event OnSpawned onSpawned;

    private void Start()
    {
        StartCoroutine(SpawnPlatforms());
    }

    private IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            onSpawned?.Invoke();
            StartCoroutine(CreatePlatform());
            yield return new WaitForSeconds(Random.Range(0, 3));
        }
    }

    private IEnumerator CreatePlatform()
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        platform.transform.localScale = new Vector3(Random.Range(1, 7), 0.5f, 1);

        platform.transform.position = new Vector3(Random.Range(-6, 6), 5, 0);
        Destroy(platform.GetComponent<Collider>());
        yield return null;
        platform.AddComponent<BoxCollider2D>().usedByEffector = true;
        PlatformEffector2D platformEffector2D = platform.AddComponent<PlatformEffector2D>();

        yield return null;
        platformEffector2D.surfaceArc = 1;

        Rigidbody2D rigidbody2D = platform.AddComponent<Rigidbody2D>();
        yield return null;
        rigidbody2D.isKinematic = true;
        rigidbody2D.velocity = Vector2.down;
        
        Destroy(platform, 10);

    }
}

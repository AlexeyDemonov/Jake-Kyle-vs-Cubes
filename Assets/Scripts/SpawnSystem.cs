using System.Collections;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public bool Spawn = true;

    [Header("Object")]
    public GameObject SpawnObject;

    [Header("Delay")]
    public float MinSpawnDelay;
    public float MaxSpawnDelay;

    [Header("Coordinate constraints")]
    public float MinX;
    public float MaxX;
    public float MinZ;
    public float MaxZ;
    public float Y;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnOverTime());
    }

    IEnumerator SpawnOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));

            if (!Spawn)
                continue;

            Vector3 position = new Vector3(Random.Range(MinX, MaxX), Y, Random.Range(MinZ, MaxZ));
            this.transform.position = position;

            if (Physics.Raycast(this.transform.position, -this.transform.up, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "Ground")
                {
                    Instantiate(SpawnObject, position, Quaternion.identity);
                }
            }
        }
    }
}
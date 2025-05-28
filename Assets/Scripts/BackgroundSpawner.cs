using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{

    bool spawned = false;
    public Transform spawnPoint;
    public GameObject backgroumd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < -1000)
        {
            Destroy(gameObject);
        }
        if (spawned) return;
        if (transform.localPosition.x < 1300)
        {
            spawned = true;
            Instantiate(backgroumd, spawnPoint.position, spawnPoint.rotation, transform.parent);
        }
    }
}

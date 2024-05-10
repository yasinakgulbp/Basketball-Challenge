using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject cowPrefab; // Koyun prefabý
    public int numberOfCowsToSpawn = 5; // Koyun sayýsý
    public GameObject cowBossPrefab; // Boss koyun prefabý
    public int numberOfCowBossesToSpawn = 1; // Boss koyun sayýsý
    public Vector3 spawnAreaSize = new Vector3(20f, 1f, 20f); // Spawn edilecek alanýn boyutu

    void Start()
    {
        SpawnCows();
        SpawnCowBosses();
    }

    void SpawnCows()
    {
        for (int i = 0; i < numberOfCowsToSpawn; i++)
        {
            SpawnCow(cowPrefab);
        }
    }

    void SpawnCowBosses()
    {
        for (int i = 0; i < numberOfCowBossesToSpawn; i++)
        {
            SpawnCow(cowBossPrefab);
        }
    }

    void SpawnCow(GameObject prefab)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                                            spawnAreaSize.y / 2f,
                                            Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)); // Rastgele spawn pozisyonu seçme
        Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f); // Rastgele dönme

        Instantiate(prefab, spawnPosition, spawnRotation); // Koyunu spawn etme
    }
}

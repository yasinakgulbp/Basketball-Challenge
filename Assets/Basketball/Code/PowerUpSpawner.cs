using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab; // Power-up prefab�
    public float spawnInterval = 30f; // Spawn aral��� (saniye)
    public Vector3 spawnAreaSize = new Vector3(20f, 0f, 20f); // Spawn alan� boyutu
    public float spawnHeight = 2f; // Yerden y�kseklik

    // Start is called before the first frame update
    void Start()
    {
        // �lk spawn i�lemini ba�lat
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            // Rastgele bir pozisyon olu�tur
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Power-up prefab�n� spawn et
            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

            // Spawn etme sesi oynat
            AudioManager2.instance.PlaySoundEffect(5);

            // Bir sonraki spawn i�in belirlenen aral�k kadar bekle
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // X ve Z eksenlerindeki rastgele bir konum
        float randomX = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float randomZ = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f);

        // Y ekseninde sabit y�kseklik
        float yPos = spawnHeight;

        // Olu�turulan pozisyonu geri d�nd�r
        return new Vector3(randomX, yPos, randomZ);
    }

    // Saha boyutunu g�stermek i�in gizli �izgileri �izmek i�in OnDrawGizmos kullan�l�r
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}

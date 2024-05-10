using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab; // Power-up prefabý
    public float spawnInterval = 30f; // Spawn aralýðý (saniye)
    public Vector3 spawnAreaSize = new Vector3(20f, 0f, 20f); // Spawn alaný boyutu
    public float spawnHeight = 2f; // Yerden yükseklik

    // Start is called before the first frame update
    void Start()
    {
        // Ýlk spawn iþlemini baþlat
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            // Rastgele bir pozisyon oluþtur
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Power-up prefabýný spawn et
            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

            // Spawn etme sesi oynat
            AudioManager2.instance.PlaySoundEffect(5);

            // Bir sonraki spawn için belirlenen aralýk kadar bekle
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // X ve Z eksenlerindeki rastgele bir konum
        float randomX = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float randomZ = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f);

        // Y ekseninde sabit yükseklik
        float yPos = spawnHeight;

        // Oluþturulan pozisyonu geri döndür
        return new Vector3(randomX, yPos, randomZ);
    }

    // Saha boyutunu göstermek için gizli çizgileri çizmek için OnDrawGizmos kullanýlýr
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}

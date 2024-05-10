using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    public static AudioManager2 instance;

    public AudioSource[] soundEffects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Ýstediðiniz ses efektlerini atanabilirsiniz.
        soundEffects = GetComponents<AudioSource>();
    }

    public void PlaySoundEffect(int index)
    {
        if (index < 0 || index >= soundEffects.Length)
        {
            Debug.LogWarning("Geçersiz ses efekti indeksi.");
            return;
        }

        soundEffects[index].Play();
    }
}

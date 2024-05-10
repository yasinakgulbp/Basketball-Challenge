using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private JoystickController joystickController;
    private CharacterController characterController;
    [SerializeField] private PlayerAnimator playerAnimator;
    Vector3 moveVector;

    [Header("Settings")]
    [SerializeField] private float normalMoveSpeed = 5f; // Normal hareket h�z�
    [SerializeField] private float powerUpMoveSpeed = 10f; // Power up al�nd���nda h�z�n artaca�� de�er
    [SerializeField] private string powerUpTag = "PowerUp"; // Power up objesinin etiketi

    private float gravity = -9.81f;
    private float gravityMultiplier = 3f;
    private float gravityVelocity;

    private bool isPoweredUp = false; // Power up al�n�p al�nmad���n� kontrol etmek i�in flag
    private float powerUpDuration = 10f; // Power up s�resi (saniye)
    private float powerUpTimer = 0f; // Power up s�resini saymak i�in saya�

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        UpdatePowerUpTimer();
    }

    private void MovePlayer()
    {
        if (!isPoweredUp)
        {
            moveVector = joystickController.GetMovePosition() * normalMoveSpeed * Time.deltaTime / Screen.width;
        }
        else
        {
            moveVector = joystickController.GetMovePosition() * powerUpMoveSpeed * Time.deltaTime / Screen.width;
        }

        moveVector.z = moveVector.y;
        moveVector.y = 0;

        playerAnimator.ManageAnimations(moveVector);

        ApplyGraviy();
        characterController.Move(moveVector);
    }

    private void ApplyGraviy()
    {
        if (characterController.isGrounded && gravityVelocity < 0.0f)
        {
            gravityVelocity = -1f;
        }
        else
        {
            gravityVelocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        moveVector.y += gravityVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        // E�er oyuncu belirli bir objeye temas ederse
        if (other.CompareTag(powerUpTag))
        {
            // H�z� iki kat�na ��kar
            isPoweredUp = true;

            // Power up objesini yok et (istedi�iniz gibi)
            Destroy(other.gameObject);

            //�arp��ma oldupunda ses oynat
            AudioManager2.instance.PlaySoundEffect(2);
        }
    }

    private void UpdatePowerUpTimer()
    {
        // E�er power up aktifse ve s�re dolmad�ysa, zamanlay�c�y� g�ncelle
        if (isPoweredUp && powerUpTimer < powerUpDuration)
        {
            powerUpTimer += Time.deltaTime;
        }
        else if (isPoweredUp) // Power up s�resi dolduysa
        {
            // H�z� normale d�nd�r
            isPoweredUp = false;
            powerUpTimer = 0f;
            AudioManager2.instance.PlaySoundEffect(6);
        }
    }
}


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
    [SerializeField] private float normalMoveSpeed = 5f; // Normal hareket hýzý
    [SerializeField] private float powerUpMoveSpeed = 10f; // Power up alýndýðýnda hýzýn artacaðý deðer
    [SerializeField] private string powerUpTag = "PowerUp"; // Power up objesinin etiketi

    private float gravity = -9.81f;
    private float gravityMultiplier = 3f;
    private float gravityVelocity;

    private bool isPoweredUp = false; // Power up alýnýp alýnmadýðýný kontrol etmek için flag
    private float powerUpDuration = 10f; // Power up süresi (saniye)
    private float powerUpTimer = 0f; // Power up süresini saymak için sayaç

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
        // Eðer oyuncu belirli bir objeye temas ederse
        if (other.CompareTag(powerUpTag))
        {
            // Hýzý iki katýna çýkar
            isPoweredUp = true;

            // Power up objesini yok et (istediðiniz gibi)
            Destroy(other.gameObject);

            //çarpýþma oldupunda ses oynat
            AudioManager2.instance.PlaySoundEffect(2);
        }
    }

    private void UpdatePowerUpTimer()
    {
        // Eðer power up aktifse ve süre dolmadýysa, zamanlayýcýyý güncelle
        if (isPoweredUp && powerUpTimer < powerUpDuration)
        {
            powerUpTimer += Time.deltaTime;
        }
        else if (isPoweredUp) // Power up süresi dolduysa
        {
            // Hýzý normale döndür
            isPoweredUp = false;
            powerUpTimer = 0f;
            AudioManager2.instance.PlaySoundEffect(6);
        }
    }
}


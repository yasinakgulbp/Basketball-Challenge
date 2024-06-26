using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerAC;
    public Vector3 TargetPosBasket;
    // Start is called before the first frame update
    void Start()
    {
        TargetPosBasket = playerAC.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ManageAnimations(Vector3 move)
    {
        if (move.magnitude > 0)
        {
            PlayRunAnimation();

            playerAC.transform.forward = move.normalized;
        }
        else
        {
            PlayIdleAnimation();
            playerAC.transform.forward = TargetPosBasket;
        }
    }

    private void PlayRunAnimation()
    {
        playerAC.Play("RUN");
    }
    
    private void PlayIdleAnimation()
    {
        playerAC.Play("IDLE");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class BasketballController : MonoBehaviour {

    public Leaderboard leaderboard;
    public float MoveSpeed = 10;
    public float GoalCount = 0;
    public Transform Ball;
    public Transform PosDribble;
    public Transform PosOverHead;
    public Transform Arms;
    public Transform Target;

    public TMP_Text GoalText;

    public CameraShake cameraShake;
    public GameObject throwButton;

    //Extra dsitance veriables
    public float maxDistance = 10f; // Maksimum mesafe
    public int maxScore = 100; // Maksimum puan
    public float distance;

    // variables
    private bool IsBallInHands = true;
    private bool IsBallFlying = false;
    private float T = 0;

    // Update is called once per frame
    void Update() {



        // Top eldeyken
        if (IsBallInHands)
        {
            // Dribble
            Ball.position = PosDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
            Arms.localEulerAngles = Vector3.right * 0;
        }

        // ball in the air
        if (IsBallFlying) {
            T += Time.deltaTime;
            float duration = 0.66f;
            float t01 = T / duration;

            // move to target
            Vector3 A = PosOverHead.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            Ball.position = pos + arc;

            // moment when ball arrives at the target
            if (t01 >= 1) {
                AudioManager2.instance.PlaySoundEffect(0);
                // Basket atýldýðýnda kamera sarsýntýsý efektini baþlat
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
                cameraShake.ShakeCamera(0.5f, 2f, 0.2f);
                GoalCount += distance;
                GoalText.text = GoalCount.ToString();
                leaderboard.GoalUptade(GoalCount);
            }
        }


    }

    private void OnTriggerEnter(Collider other) {

        if (!IsBallInHands && !IsBallFlying) {

            IsBallInHands = true;
            Ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }


    // ... Diðer fonksiyonlarýnýz

    // Butona basýldýðýnda tetiklenen fonksiyon:
    // Butona Basma
    public void OnThrowButtonPressed()
    {
        if (IsBallInHands)
        {
            Ball.position = PosOverHead.position;
            Arms.localEulerAngles = Vector3.right * 180;
            Debug.Log("Butona basýldý");
            StartCoroutine(nameof(OnThrowButtonHoldCoroutine));
        }
    }

    IEnumerator OnThrowButtonHoldCoroutine()
    {
        while (true)
        {
            OnThrowButtonHold();
            yield return null;
        }
    }

    // Butonu Basýlý Tutma
    public void OnThrowButtonHold()
    {
        if (IsBallInHands)
        {
            // Topu baþýn üzerinde tutma
            Ball.position = PosOverHead.position;
            Arms.localEulerAngles = Vector3.right * 180;
        }
    }

    // Butondan Parmak Kaldýrma
    public void OnThrowButtonReleased()
    {
        StopCoroutine(nameof(OnThrowButtonHoldCoroutine));
        if (IsBallInHands)
        {
            // Hedefe Bakma
            //transform.LookAt(Target.parent.position);
            Debug.Log("Butondan parmak kaldýrýldý");
            AudioManager2.instance.PlaySoundEffect(1);
            IsBallInHands = false;
            IsBallFlying = true;
            T = 0;
            // Oyuncu ile hedef noktasý arasýndaki mesafeyi hesapla
            distance = Mathf.RoundToInt(Vector3.Distance(Ball.position, Target.position));
            // Dribble
            Ball.position = PosDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
            Arms.localEulerAngles = Vector3.right * 0;
        }
    }



}

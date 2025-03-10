using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPaddle : MonoBehaviour
{
    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null) 
            {
                // 점프를 위한 힘 설정
                Vector3 jumpForce = new Vector3(0, 10f, 0);
                
                // 힘을 Impulse로 가하기
                playerRb.AddForce(jumpForce, ForceMode.Impulse); 
            }
        }
    }
}

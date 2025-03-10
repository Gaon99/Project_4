using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPaddle : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject Panel;
    public bool IsPad = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Panel.SetActive(true);
            IsPad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Panel.SetActive(false);
            IsPad = false;
        }
    }
}


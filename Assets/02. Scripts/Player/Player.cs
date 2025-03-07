using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public PlayerController Controller;
    public PlayerCondition condition;

    public ItemData data;
    public Action addItem;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        Controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}

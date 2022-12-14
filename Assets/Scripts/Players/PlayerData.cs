using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerData")]

public class PlayerData : ScriptableObject
{
    public Transform playerTransform { get; set; }

    public float playerSpeed;

    public float playerAccel;
}

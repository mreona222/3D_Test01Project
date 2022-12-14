using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyScriptable/Create CameraData")]
public class CameraData : ScriptableObject
{
    public Transform cameraTransform { get; set; }
}

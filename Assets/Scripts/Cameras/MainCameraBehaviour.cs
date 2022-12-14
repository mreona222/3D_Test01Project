using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Cameras
{
    public class MainCameraBehaviour : MonoBehaviour
    {
        [field: SerializeField] PlayerData playerData;

        Vector3 offset;

        float CameraX, CameraY;

        // Start is called before the first frame update
        void Start()
        {
            offset = new Vector3(0, 3.5f, -6.0f);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (SceneManager.GetSceneByName("Game01").isLoaded)
            {
                //transform.position = playerData.playerTransform.position + new Vector3(0, 3.0f, -5.0f);
                offset = Quaternion.AngleAxis(CameraX / 10.0f, transform.up) * Quaternion.AngleAxis(-CameraY / 10.0f, transform.right) * offset;
                transform.position = playerData.playerTransform.position + offset;
                transform.LookAt(playerData.playerTransform.position + new Vector3(0, 2.0f, 0));
            }
        }

        public void OnCameraX(InputAction.CallbackContext context)
        {
            CameraX = context.ReadValue<float>();
        }

        public void OnCameraY(InputAction.CallbackContext context)
        {
            CameraY = context.ReadValue<float>();
        }
    }
}
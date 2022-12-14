using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.Enviroments
{
    public class SubEnviromentsDestroyer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (SceneManager.GetSceneByName("Common").isLoaded)
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
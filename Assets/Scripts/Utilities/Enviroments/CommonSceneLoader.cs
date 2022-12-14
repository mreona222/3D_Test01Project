using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.Enviroments
{
    public class CommonSceneLoader : MonoBehaviour
    {
        private static bool Loaded { get; set; }

        void Awake()
        {
            if (Loaded) return;
            Loaded = true;

            if (SceneManager.GetSceneByName("Common").isLoaded) return;
            StartCoroutine(CommonSceneLoad());
        }

        IEnumerator CommonSceneLoad()
        {
            yield return SceneManager.LoadSceneAsync("Common", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Common"));
        }
    }
}
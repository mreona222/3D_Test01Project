using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class FadeAnimation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FadeOutAnimationEnd()
        {
            if (!SceneController.Instance.withProgress)
            {
                StartCoroutine(SceneController.Instance.SceneTransition(SceneController.Instance.nextScenes));
            }
            else
            {
                StartCoroutine(SceneController.Instance.SceneTransitionWithProgress(SceneController.Instance.nextScenes));
            }
        }

        void FadeInAnimationEnd()
        {
            Destroy(this.transform.root.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Managers;

namespace Menus
{
    public class MenuUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClickGameTransition()
        {
            SceneController.Instance.ToGame();
        }

        public void OnClickTitleTransition()
        {
            SceneController.Instance.ToTitle();
        }

        public void OnClickQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		Application.OpenURL("http://www.google.co.jp/");
#else
		Application.Quit();
#endif
        }
    }
}
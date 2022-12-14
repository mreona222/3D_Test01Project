using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Managers;

namespace Titles
{
    public class TitleUI : MonoBehaviour
    {
        [field: SerializeField] GameObject nowLoadingCanvas;
        GameObject nowLoadingCanvasInstantiate;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClick()
        {
            SceneController.Instance.ToMenu();
        }
    }
}
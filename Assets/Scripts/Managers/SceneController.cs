using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Utilities.Singleton;

namespace Managers
{
    public enum Scenes
    {
        Common,
        Title01,
        TitleUI01,
        Menu01,
        MenuUI01,
        Game01,
        GameUI01,

    }

    public class SceneController : SingletonMonoBehaviour<SceneController>
    {
        bool fadeOut;
        bool fadeIn;
        bool sceneTransitioned;
        bool sceneUnloaded;

        public List<string> nextScenes = new List<string>();

        private Slider progressBar;
        float totalSceneProgress;

        public bool withProgress;

        [field: SerializeField] GameObject fadeGameObject;
        internal GameObject fadeInstance;

        [field: SerializeField] GameObject loadingScreenGameObject;
        GameObject loadingScreenInstance;

        List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

        private void Start()
        {
            if (SceneManager.sceneCount == 1)
            {
                StartCoroutine(SceneTransition(new List<string> { Scenes.Title01.ToString(), Scenes.TitleUI01.ToString() }));
            }

            else if (
                SceneManager.GetSceneByName(Scenes.Title01.ToString()).isLoaded || 
                SceneManager.GetSceneByName(Scenes.TitleUI01.ToString()).isLoaded)
            {
                StartCoroutine(SceneTransition(new List<string> { Scenes.Title01.ToString(), Scenes.TitleUI01.ToString() }));
            }

            else if (
                SceneManager.GetSceneByName(Scenes.Menu01.ToString()).isLoaded ||
                SceneManager.GetSceneByName(Scenes.MenuUI01.ToString()).isLoaded)
            {
                StartCoroutine(SceneTransition(new List<string> { Scenes.Menu01.ToString(), Scenes.MenuUI01.ToString() }));
            }

            else if (
                SceneManager.GetSceneByName(Scenes.Game01.ToString()).isLoaded ||
                SceneManager.GetSceneByName(Scenes.GameUI01.ToString()).isLoaded)
            {
                StartCoroutine(SceneTransition(new List<string> { Scenes.Game01.ToString(), Scenes.GameUI01.ToString() }));
            }

            else
            {
                Debug.LogError("シーンのロードに失敗しました。");
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------
        public void ToTitle()
        {
            withProgress = false;
            nextScenes = new List<string>
            {
                Scenes.Title01.ToString(),
                Scenes.TitleUI01.ToString(),
            };
            FadeOut();
        }

        public void ToMenu()
        {
            withProgress = false;
            nextScenes = new List<string>
            {
                Scenes.Menu01.ToString(),
                Scenes.MenuUI01.ToString(),
            };
            FadeOut();
        }

        public void ToGame()
        {
            withProgress = true;
            nextScenes = new List<string>
            {
                Scenes.Game01.ToString(),
                Scenes.GameUI01.ToString(),
            };
            FadeOut();
        }


        //----------------------------------------------------------------------------------------------------------------------------------------------------
        public IEnumerator SceneTransition(List<string> nextScenes)
        {
            if (fadeInstance == null)
            {
                fadeInstance = Instantiate(fadeGameObject);
            }
            loadingScreenInstance = Instantiate(loadingScreenGameObject);

            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                if (SceneManager.GetSceneAt(i).name != Scenes.Common.ToString())
                {
                    scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name));
                }
            }

            for (int i = 0; i < nextScenes.Count; ++i)
            {
                if (!SceneManager.GetSceneByName(nextScenes[i]).isLoaded)
                {
                    scenesLoading.Add(SceneManager.LoadSceneAsync(nextScenes[i], LoadSceneMode.Additive));
                }
            }

            yield return StartCoroutine(LoadScenes());
            FadeIn();
        }

        public IEnumerator SceneTransitionWithProgress(List<string> nextScenes)
        {
            loadingScreenInstance = Instantiate(loadingScreenGameObject);

            for(int i = 0; i < SceneManager.sceneCount; ++i)
            {
                if (SceneManager.GetSceneAt(i).name != Scenes.Common.ToString())
                {
                    scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name));
                }
            }

            for (int i = 0; i < nextScenes.Count; ++i)
            {
                if (!SceneManager.GetSceneByName(nextScenes[i]).isLoaded)
                {
                    scenesLoading.Add(SceneManager.LoadSceneAsync(nextScenes[i], LoadSceneMode.Additive));
                }
            }

            yield return StartCoroutine(GetSceneLoadProgress());
            FadeIn();
        }

        private IEnumerator LoadScenes()
        {
            for (int i = 0; i < scenesLoading.Count; ++i)
            {
                while (!scenesLoading[i].isDone)
                {
                    yield return null;
                }
            }

            Destroy(loadingScreenInstance);
        }

        private IEnumerator GetSceneLoadProgress()
        {
            progressBar = loadingScreenInstance.GetComponentInChildren<Slider>();
            progressBar.maxValue = scenesLoading.Count;

            for(int i = 0; i < scenesLoading.Count; ++i)
            {
                while (!scenesLoading[i].isDone)
                {
                    totalSceneProgress = 0;

                    foreach(AsyncOperation operation in scenesLoading)
                    {
                        totalSceneProgress += operation.progress;
                    }

                    totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100.0f;

                    progressBar.value = Mathf.RoundToInt(totalSceneProgress);

                    yield return null;
                }
            }

            Destroy(loadingScreenInstance);
        }

        public void FadeOut()
        {
            fadeInstance = Instantiate(fadeGameObject);
        }

        public void FadeIn()
        {
            fadeInstance.GetComponentInChildren<Animator>().SetTrigger("Fade");
        }
    }
}
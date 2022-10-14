using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    private bool onTransition = false;
    // Start is called before the first frame update
    public void GoToScene(int sceneIndex)
    {
        if (!onTransition)
        {
            onTransition = true;
            StartCoroutine(GoToSceneRoutine(sceneIndex));
        }
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        float timer = 0;
        while (timer <= fadeScreen.fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneIndex);

        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        //operation.allowSceneActivation = false;

        //float timer = 0;
        //while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        //{
        //    timer += Time.unscaledDeltaTime;
        //    yield return null;
        //}

        //operation.allowSceneActivation = true;
    }
}

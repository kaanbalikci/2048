using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider filledBar;

    public void LoadScene(int sceneID)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    public IEnumerator LoadSceneAsync(int sceneID)
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        

        while (!operation.isDone)
        {
            
            float value = Mathf.Clamp01(operation.progress / 0.9f);

            filledBar.value = value;

            yield return null;
        }

        
    }
}

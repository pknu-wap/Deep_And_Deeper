using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   [SerializeField] private Image progressbar;
   //[SerializeField] private TextMeshProUGUI loadtext;
   public static string loadScene;
   //public static int loadType; //0: 새게임 1: 하던 거 이어하기
   
   private void Start()
   {
      StartCoroutine(LoadScene());
   }

   public static void LoadSceneHandle(string loadname)
   {
      //loadType = _loadtype;
      loadScene = loadname;
      SceneManager.LoadScene("Loading");
   }

   IEnumerator LoadScene() //비동기 로드
   {
      yield return null;
      AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);

      operation.allowSceneActivation = false;

      while (!operation.isDone)
      {
         yield return null;
         
         /*if(loadType == 0)
            Debug.Log("new start");
         else if(loadType == 1)
            Debug.Log("continue");*/
         
         if (progressbar.fillAmount < 0.9f)
         {
            progressbar.fillAmount = Mathf.MoveTowards(progressbar.fillAmount, 0.9f, Time.deltaTime);
         }
         else if(operation.progress >= 0.9f)
         {
            progressbar.fillAmount = Mathf.MoveTowards(progressbar.fillAmount, 1f, Time.deltaTime);
            operation.allowSceneActivation = true;

         }
      }
   }
}

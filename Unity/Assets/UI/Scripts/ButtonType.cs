using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public BtnType currentType;
   private Transform _transformOfBtnn;
   private Vector3 defaultScale;

   private void Start()
   {
      _transformOfBtnn = gameObject.GetComponent<Transform>();
      defaultScale = _transformOfBtnn.localScale;
   }

   public void OnBtnClick()
   {
      switch (currentType)
      {
         case BtnType.NewStart:
            SceneLoader.LoadSceneHandle("BattleMap2_1"); //나중에 수정할 거
            break;
         case BtnType.Tutorial:
            SceneLoader.LoadSceneHandle("Tutorial");
            break;
         case BtnType.Exit:
            Application.Quit();
            Debug.Log("exit");
            break;
      }
   }

    //버튼 크기 키우기
   public void OnPointerEnter(PointerEventData eventData)
   {
      _transformOfBtnn.localScale = defaultScale * 1.2f;
   }
   public void OnPointerExit(PointerEventData eventData)
   {
      _transformOfBtnn.localScale = defaultScale;
   }
}

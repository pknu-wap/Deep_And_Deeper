using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            Debug.Log("new start");
            break;
         case BtnType.Continue:
            Debug.Log("continue");
            break;
         case BtnType.Exit:
            Debug.Log("exit");
            break;
      }
   }

   public void OnPointerEnter(PointerEventData eventData)
   {
      _transformOfBtnn.localScale = defaultScale * 1.2f;
   }
   public void OnPointerExit(PointerEventData eventData)
   {
      _transformOfBtnn.localScale = defaultScale;
   }
}

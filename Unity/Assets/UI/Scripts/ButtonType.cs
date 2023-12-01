using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BtnType currentType;
    private Transform _buttonTransform;
    private Vector3 _defaultScale;

    private void Start()
    {
        _buttonTransform = gameObject.GetComponent<Transform>();
        _defaultScale = _buttonTransform.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BtnType.NewStart:
                SceneLoader.LoadSceneHandle("RandomMapTest");
                break;
            case BtnType.Tutorial:
                SceneLoader.LoadSceneHandle("Tutorial");
                break;
            case BtnType.Exit:
                Application.Quit();
                Debug.Log("exit");
                break;
            case BtnType.Main:
                SceneLoader.LoadSceneHandle("Main");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //버튼 크기 키우기
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonTransform.localScale = _defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonTransform.localScale = _defaultScale;
    }
}
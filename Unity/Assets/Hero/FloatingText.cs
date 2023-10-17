using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Floatingtext
{
    // Start is called before the first frame update
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }
    

    // Update is called once per frame
  public void UpdateFloatingText()
  {
      if (!active)
          return;
      
      if(Time.time - lastShown > duration)
          Hide();

      go.transform.position += motion * Time.deltaTime;
  }
}

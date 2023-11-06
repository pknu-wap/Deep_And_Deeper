using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterAttributes : MonoBehaviour
{
    public Text[] attributeTexts; //UI에 각 특성을 표시할 텍스트 배열

    private List<string> attributes = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        attributes.Add("mp");
        attributes.Add("power");
        attributes.Add("hp");
        attributes.Add("money");
    }

    List<string> selectedAttributes = new List<string>();

    for(int i=0;i<3;i++){
        int randomIndex = Random.Range(0, attributes.Count);
        selectedAttributes.Add(attributes[randomIndex]);
        attributes.RemoveAt(randomIndex);
    }
        
        
    // Update is called once per frame
for (int i = 0; i < selectedAttributes.Count; i++)
{
    attributeTexts[i].text = selectedAttributes[i];
}
    void Update()
    {
        
    }
}

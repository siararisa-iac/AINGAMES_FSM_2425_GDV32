using System.Collections.Generic;
using UnityEngine;

public class DictionaryExample : MonoBehaviour
{
    private Dictionary<string,float> _productPrices = new Dictionary<string,float>();

    private void Start()
    {
        _productPrices.Add("Coffee", 100.0f);
        _productPrices.Add("Kwasong", 150.25f);
        _productPrices.Add("Matcha", 200.0f);
        _productPrices.Add("Boston Kreme", 1000f);

        Debug.Log( _productPrices["Coffee"]);//100
        Debug.Log(_productPrices["Kwasong"]); // 150.25f;

        _productPrices["Boston Kreme"] = 250f;
        Debug.Log(_productPrices["Boston Kreme"]); // 250f
    }


  
}

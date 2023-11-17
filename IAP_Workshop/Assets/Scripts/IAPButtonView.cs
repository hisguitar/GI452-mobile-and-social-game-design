using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class IAPButtonView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text price;
    [SerializeField] private TMP_Text detail;

    public void OnProductFetched(Product product)
    {
        if(title != null)
        {
            title.text = product.metadata.localizedTitle;
        }

        if(price != null)
        {
            price.text = $"{product.metadata.localizedPriceString} USD";
        }

        if(detail != null)
        {
            detail.text = product.metadata.localizedDescription;
        }
    }
}

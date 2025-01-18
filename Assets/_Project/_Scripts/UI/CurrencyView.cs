using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MVVM;
using TMPro;
using UnityEngine;
using UniRx;

namespace  UIControl
{
    public class CurrencyView : MonoBehaviour
    {
        [Data("Currency")]
        [SerializeField] 
        private TextMeshProUGUI _currencyValue;
    }
}

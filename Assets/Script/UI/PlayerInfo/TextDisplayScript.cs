using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayScript : MonoBehaviour
{
    public TextMeshProUGUI thisTMPro;
    public float _value = 1f;

    public enum displaytype
    {
        AttackSpeed,
        DamageMultiplier
    }
    public displaytype _displayType;
    // Start is called before the first frame update
    void Start()
    {
        thisTMPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        displayByType(_displayType);
    }

    public void displayByType(displaytype _type)
    {
        switch (_type)
        {
            case displaytype.AttackSpeed:
                thisTMPro.text = "Speed: " + _value.ToString("F2");
                break;
            case displaytype.DamageMultiplier:
                thisTMPro.text = "Damage: " + _value.ToString("F1");
                break;
            default:
                thisTMPro.text = "Something: " + _value.ToString();
                break;
        }
    }
    public void setValue(float _inputValue)
    {
        _value = _inputValue;
    }
}

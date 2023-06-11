using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlash : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Flash();
    }

    void Flash()
    {
        float expression = (Mathf.Cos(Time.time * 2 * Mathf.PI) + 1) * 0.5f;
        text.color = new Color(1, 1, 1, expression);
    }
}

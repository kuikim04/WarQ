using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(SetFalse), 2f);
    }
    private void SetFalse()
    {
        gameObject.SetActive(false);
    }
}

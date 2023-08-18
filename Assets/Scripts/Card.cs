using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] protected GameObject _selectedOverlay;

    protected void ResetCards()
    {
        Debug.Log("ResetCards");
        _selectedOverlay.SetActive(false);
    }
}

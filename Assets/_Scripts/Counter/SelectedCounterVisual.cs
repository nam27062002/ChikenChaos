using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Counter;
using _Scripts.Player;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject selectedCounter;
    [SerializeField] private BaseCounter baseCounter;
    
    private void Start()
    {
        PlayerMovement.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerMovement.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        selectedCounter.SetActive(true);
    }
    public void Hide()
    {
        selectedCounter.SetActive(false);
    }
}

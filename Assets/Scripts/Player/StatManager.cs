using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class StatManager : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image manaBarImage;
    private PlayerStatus playerStatus;

    private void OnEnable()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        playerStatus.OnPlayerStatusChange += OnPlayerStatusChange;
    }

    private void Start()
    {
        healthBarImage.fillAmount = playerStatus.Health / 100f;
        manaBarImage.fillAmount= playerStatus.Mana / 100f;
    }

    private void OnDisable()
    {
        playerStatus.OnPlayerStatusChange -= OnPlayerStatusChange;
    }

    private void OnPlayerStatusChange(int amount, PlayerStatus.PlayerStatusType playerStatusType)
    {
        if (playerStatusType == PlayerStatus.PlayerStatusType.Health)
        {
            Debug.Log("Health: " + playerStatus.Health + " MaxHealth: " + playerStatus.MaxHealth);
            healthBarImage.fillAmount = amount / 100f;
        }
        else if (playerStatusType == PlayerStatus.PlayerStatusType.Mana)
        {
            Debug.Log("Mana: " + amount);
            manaBarImage.fillAmount = amount / 100f;
        }
    }
}

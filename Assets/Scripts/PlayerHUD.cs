using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        currentHealthText.SetText(currentHealth.ToString());
        maxHealthText.SetText(maxHealth.ToString());
    }
}


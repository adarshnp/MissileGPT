using System;
using TMPro;
using UnityEngine;

namespace missilegpt
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI debugText;
        public GameObject restartButton;
        private void OnEnable()
        {
            GameManager.onGameRestart += ResetUI;
            PlayerHealth.onScoreUpdate += UpdateUI;
            PlayerHealth.onPlayerDeath += UpdateUI;
        }
        void UpdateUI(float distance, float radius, float damage, float health)
        {
            debugText.text = $"Distance from hit : {Math.Round(distance, 2)}\nmissile radius : {Math.Round(radius, 2)}\nDamage Calculated: {Math.Round(damage, 2)}\ncurrentHealth : {Math.Round(health, 2)}";
        }
        void UpdateUI()
        {
            debugText.text = "PLAYER DIED!!!";
            restartButton.SetActive(true);
        }
        void ResetUI()
        {
            debugText.text = "";
            restartButton.SetActive(false);
        }
        private void OnDisable()
        {
            GameManager.onGameRestart -= ResetUI;
            PlayerHealth.onScoreUpdate -= UpdateUI;
            PlayerHealth.onPlayerDeath -= UpdateUI;
        }
    }
}
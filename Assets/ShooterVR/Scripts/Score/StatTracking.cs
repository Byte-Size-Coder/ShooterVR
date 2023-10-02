using UnityEngine;
using TMPro;
using System;

namespace BSC.SVR.Score
{
    public class StatTracking : MonoBehaviour
    {
        [SerializeField] private TMP_Text barrelText;
        [SerializeField] private TMP_Text shotText;
        [SerializeField] private TMP_Text accuracyText;
        [SerializeField] private TMP_Text timeText;

        [SerializeField] private GameObject startSection;
        [SerializeField] private GameObject statSection;

        [SerializeField] private float GameTimeSeconds;

        private int barrelDestroyed = 0;
        private int shotsFired = 0;

        private float timeRemaining;
        private bool gameStart = false;


        public static StatTracking Instance { get; private set; }
        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            if (gameStart && timeRemaining < 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            
        }

        public void StartGame()
        {
            startSection.SetActive(false);
            startSection.SetActive(true);
            timeRemaining = GameTimeSeconds;
        }

        public void BarrelDestroyed()
        {
            barrelDestroyed++;
            UpdateStats();
        }

        public void ShotFired()
        {
            shotsFired++;
            UpdateStats();
        }

        private void UpdateStats()
        {
            barrelText.text = $"Barrels Destroyed: {barrelDestroyed}";
            shotText.text = $"Shots Fired: {shotsFired}";

            float accuracy = ((float)barrelDestroyed / shotsFired);

            accuracyText.text = $"Accuracy: {(accuracy * 100).ToString("0.00")}%";
        }

        private void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}


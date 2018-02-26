﻿using UnityEngine;

public class Stage2Orchestrator : MonoBehaviour {

    private GameObject _pauseMenu;
    private GameObject _settingsMenu;
    private GameObject _inventory;

    private AudioSource _backgroundMusic;
    private PlayerStats _playerStats;

    private GameObject _deathScreen;

    private float _lastVolumeValue = 1;

    private void Start() {
        InitializeComponents();
        InitializePreferences();

        Time.timeScale = 1;
    }

    private void Update() {
        if (_playerStats.IsDead()) {
            _deathScreen.SetActive(true);
            Time.timeScale = 0;
            return;
        }

        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.Escape)) {
            if (_pauseMenu.activeSelf) {
                ResumeGame();

            } else {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0;

                _lastVolumeValue = _backgroundMusic.volume;
                _backgroundMusic.volume = 0;
            }
        }

        if (!_pauseMenu.activeSelf && Input.GetKeyDown(KeyCode.I)) {
            _inventory.SetActive(!_inventory.activeSelf);
        }
    }

    private void InitializeComponents() {
        _pauseMenu = GameObject.Find("PauseMenu");
        _pauseMenu.SetActive(false);

        _settingsMenu = GameObject.Find("SettingsMenu");
        _settingsMenu.SetActive(false);

        _backgroundMusic = GameObject.Find("BackgroundMusic")
                            .GetComponent<AudioSource>();

        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        _deathScreen = GameObject.Find("DeathScreen");
        _deathScreen.SetActive(false);

        _inventory = GameObject.Find("Inventory");
        _inventory.SetActive(false);

        GameObject.Find("GamePlay").SetActive(false);
        GameObject.Find("Audio").SetActive(false);
    }

    private void InitializePreferences() {
        var volumeSaved = PlayerPrefs.GetFloat("MasterVolume", 1f);
        _backgroundMusic.volume = volumeSaved;
    }

    public void ResumeGame() {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;

        _backgroundMusic.volume = _lastVolumeValue;
    }

    public void SetLastSavedVolume(float value) {
        _lastVolumeValue = value;
    }

    public GameObject GetPauseMenu() {
        return _pauseMenu;
    }

    public GameObject GetSettingsMenu() {
        return _settingsMenu;
    }
}

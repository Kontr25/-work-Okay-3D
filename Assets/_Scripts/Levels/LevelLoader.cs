using System;
using BayatGames.SaveGameFree;
using System.Collections.Generic;
using _Scripts.Levels;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MMSingleton<LevelLoader>
{
    public static Action<int> LoadLevelAction;

    [SerializeField] private List<Level> _levels;
    [SerializeField] private int _levelForLoad;
    [SerializeField] private LevelButton[] _levelButtons;
    [SerializeField] private GameObject _tutorialFirst;
    [SerializeField] private GameObject _tutorialSecond;

    private int _currentLevel;
    private int _totalLevel;

    public int CurrentLevel => _currentLevel;

    private void Start()
    {
        LoadLevelAction += LoadLevelFromButton;
        for (int i = 0; i < _levels.Count; i++)
        {
            _levelButtons[i].gameObject.SetActive(true);
            _levelButtons[i].LevelNumber = i + 1;
        }
//#if UNITY_EDITOR

        //SaveGame.Clear();
        SetLevel();
//#endif
        //if (PlayerPrefs.HasKey("CurrentLevel") == false)
        //{
        //    _currentLevel = 0;
        //    PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        //    PlayerPrefs.SetInt("TotalLevel", _currentLevel + 1);
        //}
        //else
        //{
        //PlayerPrefs.GetInt("CurrentLevel");
        //}

        _currentLevel = SaveGame.Load(Keys.CurrentLevel, 0);
        _totalLevel = SaveGame.Load(Keys.TotalLevel, 0);
        _levelForLoad = _currentLevel + 1;

        LoadLevel();
    }

    private void OnEnable()
    {
        LoadLevelAction -= LoadLevelFromButton;
    }

    private void LoadLevelFromButton(int levelNumber)
    {
        _levelForLoad = levelNumber;
        SetLevel();
        RestartLevel();
    }

    private void LoadLevel()
    {
        if (_levels.Count > 0)
        {
            foreach (var level in _levels)
                level.gameObject.SetActive(false);
            _levels[_currentLevel].gameObject.SetActive(true);
            if (_currentLevel == 0)
            {
                _tutorialFirst.SetActive(true);
            }

            if (_currentLevel == 1)
            {
                _tutorialSecond.SetActive(true);
            }
        }

        Time.timeScale = 1;
    }


    private void SetLevel()
    {
        if (_levelForLoad != 0)
        {
            SaveGame.Save(Keys.CurrentLevel, _levelForLoad - 1);
        }

        //PlayerPrefs.SetInt("CurrentLevel", _levelForLoad - 1);
    }

    public void LoadNextLevel()
    {
        _currentLevel++;
        _totalLevel++;

        if (_currentLevel >= _levels.Count)
        {
            _currentLevel = 0;
        }

        SaveGame.Save(Keys.TotalLevel, _totalLevel);
        SaveGame.Save(Keys.CurrentLevel, _currentLevel);

        //PlayerPrefs.SetInt("TotalLevel", PlayerPrefs.GetInt("TotalLevel") + 1);
        //PlayerPrefs.SetInt("CurrentLevel", _currentLevel);

        SceneManager.LoadScene(1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
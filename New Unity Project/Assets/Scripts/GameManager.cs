using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [SerializeField] int _curSceneIndex;
    //[SerializeField] List<string> _levels;
    [SerializeField] FadeController _fadeController;
    [SerializeField] List<GameObject> _playerObjects;
    [SerializeField] ScriptablePlayer _scriptablePlayer;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Mult GameManager Instances");
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        _curSceneIndex = 0;
    }

    public void StartGame()
    {
        _scriptablePlayer._lifes = 3;
        NextLevel();
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    private IEnumerator NextLevelCoroutine()
    {
        _curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        _curSceneIndex++;
        if (_curSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            ToMainMenu();
            yield break;
        }
        ;
        yield return StartCoroutine(_fadeController.FadeIn());
        SceneManager.LoadScene(_curSceneIndex, LoadSceneMode.Single);
        foreach (GameObject g in _playerObjects)
            g.SetActive(true);
        yield return StartCoroutine(_fadeController.FadeOut());
    }
    
    public void ToMainMenu()
    {
        Debug.Log("To main menu");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Destroy(this.gameObject);
    }
}

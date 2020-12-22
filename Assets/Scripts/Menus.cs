using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{

    bool _isPaused;

    [SerializeField]
    GameObject _pauseMenu;

    [SerializeField]
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _isPaused = false;
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                Time.timeScale = 0;
                _isPaused = true;

                _pauseMenu.SetActive(true);

                _audioSource.volume = 0.1f;
                
            }
            else
            {
                Time.timeScale = 1;
                _isPaused = false;

                _pauseMenu.SetActive(false);

                _audioSource.volume = 0.3f;
            }
            
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        _isPaused = false;

        _pauseMenu.SetActive(false);

        _audioSource.volume = 0.3f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

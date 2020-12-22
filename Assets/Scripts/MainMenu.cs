using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioSource;

    public void Starter()
    {
        _audioSource.Play();

        Invoke("LoadScene1", 1.5f);
    }

    public void Creditos()
    {
        _audioSource.Play();
        Invoke("LoadScene2", 1.5f);
    }

    public void RobertoGaming()
    {
        _audioSource.Play();
        Invoke("LoadScene3", 1.5f);
    }

    public void BackToMenu()
    {
        
        _audioSource.Play();
        Invoke("LoadScene4", 1.5f);
    }

    public void CloseGame()
    {
        _audioSource.Play();
        Application.Quit();
    }

    void LoadScene1()
    {
        SceneManager.LoadScene("Game");
    }

    void LoadScene2()
    {
        SceneManager.LoadScene("Creditos");
    }

    void LoadScene3()
    {
        SceneManager.LoadScene("EasterEgg");
    }

    void LoadScene4()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

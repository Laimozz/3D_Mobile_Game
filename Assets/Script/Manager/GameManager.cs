using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(0);
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }
}

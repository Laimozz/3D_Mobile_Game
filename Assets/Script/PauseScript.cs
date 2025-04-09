using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject pauseImage;
    [SerializeField] private GameObject settingImage;

    private void Start()
    {
        panelPause.transform.GetComponent<Image>().raycastTarget = false;
        pauseImage.SetActive(false);
        settingImage.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        panelPause.transform.GetComponent<Image>().raycastTarget = true;
        pauseImage.SetActive(true);
        settingImage.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        panelPause.transform.GetComponent<Image>().raycastTarget = false;
        pauseImage.SetActive(false);
        settingImage.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Setting()
    {
        panelPause.transform.GetComponent<Image>().raycastTarget = true;
        pauseImage.SetActive(false);
        settingImage.SetActive(true);
        Time.timeScale = 0f;
    }
}

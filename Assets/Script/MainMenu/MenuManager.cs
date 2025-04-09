using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject setting;

    [SerializeField] private AudioClip buttonSound;

    private void Start()
    {
        if(shop != null)
        {
            shop.SetActive(false);
        }
        if(setting != null)
        {
            setting.SetActive(false);
        }
    }
    public void StartGame()
    {
        if(shop != null)
        {
            shop.SetActive(false);
        }
        if(setting != null)
        {
            setting.SetActive(false);
        }
        AudioManager.Instance.PlayClipOneShot(buttonSound);
        SceneManager.LoadScene(1);
    }

    public void OpenCloseShop()
    {
        if(shop != null)
        {
            shop.SetActive(!shop.activeSelf);    
        }
        if(setting != null)
        {
            setting.SetActive(false) ;
        }
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }

    public void OpenCloseSetting()
    {
        if(setting != null)
        {
            setting.SetActive(!setting.activeSelf);
        }
        if(shop != null)
        {
            shop.SetActive(false);
        }
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }
}

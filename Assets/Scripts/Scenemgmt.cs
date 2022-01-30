using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenemgmt : MonoBehaviour
{
    [SerializeField] GameObject Option;
    public void load(int level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        print("quit");
        Application.Quit();
    }

    public void ToggleOption()
    {
        Option.SetActive(!Option.activeInHierarchy);
    }
}

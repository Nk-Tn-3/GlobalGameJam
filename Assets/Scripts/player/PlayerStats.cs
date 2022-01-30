using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    public float maxHealthChicken = 20, maxHealthTrex = 300;
    public float currentChickenHealth, currentDinoHealth;
    int index = 0;
    [SerializeField]
    GameObject DinoImage, ChickenImage;

    [SerializeField]
    Image healthBar;
    private void Start()
    {
        currentChickenHealth = maxHealthChicken;
        currentDinoHealth = maxHealthTrex;
    }
    public void ChangeUIImage(int val)
    {
        if(val == 1)
        {

            ChickenImage.SetActive(false);
            DinoImage.SetActive(true);
            healthBar.fillAmount = currentDinoHealth / maxHealthTrex;

        }
        else
        {
            ChickenImage.SetActive(true);
            DinoImage.SetActive(false);
            healthBar.fillAmount = currentChickenHealth / maxHealthChicken;
        }

        index = val;
    }


    public void TakeDamage(float dmg)
    {
        if (index == 0) currentChickenHealth -= dmg;
        else currentDinoHealth -= dmg;

        if(currentChickenHealth<=0 || currentDinoHealth <= 0)
        {
            Die();
        }

        ChangeUIImage(index);

    }


    void Die()
    {
        SceneManager.LoadScene(0);
    }

}

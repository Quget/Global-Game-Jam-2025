using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image heart;
    public int maxHealth = 3;

    int currentHealth;
    GameObject[] hearts;

    int Health
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    void Awake()
    {
        List<GameObject> hearts = new() { heart.gameObject };

        for (int i = 0; i < maxHealth - 1; i++)
            hearts.Add(Instantiate(heart.gameObject, transform));

        this.hearts = hearts.ToArray();

        Health = maxHealth;
    }

    // Returns true if the health is zero
    public bool RemoveHeart()
    {
        Health--;
        UpdateHearts();

        return currentHealth <= 0;
    }

    public void AddHeart()
    {
        Health++;
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].SetActive(Health > i);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image heart;

    GameObject[] hearts;

    void Awake()
    {
        List<GameObject> hearts = new() { heart.gameObject };

        for (int i = 0; i < GameManager.Instance.gameData.Health - 1; i++)
            hearts.Add(Instantiate(heart.gameObject, transform));

        this.hearts = hearts.ToArray();
    }

    void Update()
    {
        if (hearts.Length > GameManager.Instance.gameData.Health)
        {
            RemoveHeart();
        } else if (hearts.Length < GameManager.Instance.gameData.Health) {
            AddHeart();
        }
    }

    // Returns true if the health is zero
    public bool RemoveHeart()
    {
//        Health--;
        UpdateHearts();

//        return currentHealth <= 0;
        return GameManager.Instance.gameData.Health <= 0;
    }

    public void AddHeart()
    {
//        Health++;
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].SetActive(GameManager.Instance.gameData.Health > i);
    }
}

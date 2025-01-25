using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    public Image[] powerups;

    void Awake()
    {
        for (int i = 0; i < powerups.Length; i++)
            DisablePowerup(i);
    }

    public void EnablePowerup(int index) => powerups[index].gameObject.SetActive(true);
    public void DisablePowerup(int index) => powerups[index].gameObject.SetActive(false);
}

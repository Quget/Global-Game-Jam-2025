using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    public Image[] powerups;

    void Update()
    {
        if (GameManager.Instance.gameData.PowerUp != 0) {
            DisablePowerup(0);
        } else {
            EnablePowerup(0);
        }
        for (int i = 1; i < powerups.Length; i++){
            DisablePowerup(i);
        }       
        for (int i = 1; i < GameManager.Instance.gameData.PowerUp+1; i++) {
            EnablePowerup(i);
        }            
    }

    public void EnablePowerup(int index) => powerups[index].gameObject.SetActive(true);
    public void DisablePowerup(int index) => powerups[index].gameObject.SetActive(false);
}

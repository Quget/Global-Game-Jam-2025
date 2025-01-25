using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HealthUI healthUI;
    public PowerupUI powerupUI;

    public static UIManager instance;

    void Awake()
    {
        instance = this;
    }

    public void EnablePowerup(int index) => powerupUI.EnablePowerup(index);
    public void DisablePowerup(int index) => powerupUI.DisablePowerup(index);

    public void AddHeart() => healthUI.AddHeart();
    public bool RemoveHeart() => healthUI.RemoveHeart(); // Returns true if all hearts are gone
}

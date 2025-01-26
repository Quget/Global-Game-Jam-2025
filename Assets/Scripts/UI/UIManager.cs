using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HealthUI healthUI;
    public PowerupUI powerupUI;
    public TutorialUI tutorialUI;

    public static UIManager instance;

    void Awake()
    {
        instance = this;
    }

    public void EnablePowerup(int index)
    {
        if (index == 1)
            tutorialUI.ShowShootTutorial();

        powerupUI.EnablePowerup(index);
    }

    public void DisablePowerup(int index)
    {
        if (index == 1)
            tutorialUI.ShowShootTutorial(false);

        powerupUI.DisablePowerup(index);
    }

    public void AddHeart() => healthUI.AddHeart();
    public bool RemoveHeart() => healthUI.RemoveHeart(); // Returns true if all hearts are gone
}

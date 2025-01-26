using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public HealthUI healthUI;
    public PowerupUI powerupUI;
    public TutorialUI tutorialUI;
    public GameOverUI gameOverUI;
    public CheesyTextUI cheesyText;

    public TutorialPrompt[] prompts;
    bool usingKeyboard = true;

    public static UIManager instance;

    void Awake()
    {
        instance = this;
        gameOverUI.gameObject.SetActive(false);
        cheesyText.gameObject.SetActive(false);

	}

    void OnEnable() => InputSystem.onEvent += OnInputSystemEvent;
    void OnDisable() => InputSystem.onEvent += OnInputSystemEvent;

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

    public void ShowText(string text)
    {
        cheesyText.ShowText(text);
    }

    public void ShowGameOver()
    {
        gameOverUI.gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        GameManager.Instance.ResetGame();
    }
    public void AddHeart() => healthUI.AddHeart();
    public bool RemoveHeart() => healthUI.RemoveHeart(); // Returns true if all hearts are gone


    void OnInputSystemEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, InputDevice device)
    {
        //device.description.deviceClass.Equals("Keyboard");
        if (!device.description.deviceClass.Equals("Keyboard") &&!device.description.deviceClass.Equals("Mouse") && usingKeyboard)
        {
            usingKeyboard = false;

            foreach (TutorialPrompt prompt in prompts)
                prompt.SetControllerControls();

            return;
        }

        if (device is Keyboard & !usingKeyboard)
        {
            usingKeyboard = true;

            foreach (TutorialPrompt prompt in prompts)
                prompt.SetKeboardControls();
        }
    }

}

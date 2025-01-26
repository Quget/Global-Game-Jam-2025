using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public HealthUI healthUI;
    public PowerupUI powerupUI;
    public TutorialUI tutorialUI;

    public TutorialPrompt[] prompts;

    public static UIManager instance;

    bool usingKeyboard = true;

    void Awake() => instance = this;

    void OnEnable() => InputSystem.onEvent += OnInputSystemEvent;
    void OnDisable() => InputSystem.onEvent += OnInputSystemEvent;

    void OnInputSystemEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, InputDevice device)
    {
        if (device is UnityEngine.InputSystem.XInput.XInputController && usingKeyboard)
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

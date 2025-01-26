using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class TutorialPrompt : MonoBehaviour
{
    [Multiline]
    public string keyboardTutorial, controllerTutorial;

    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = keyboardTutorial;
    }

    // void OnEnable()
    // {
    //     InputUser.onChange += onInputDeviceChange;
    // }

    // void OnDisable()
    // {
    //     InputUser.onChange -= onInputDeviceChange;
    // }

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        // Debug.Log("updates!");
        // if (change == InputUserChange.ControlSchemeChanged)
        // {
        //     Debug.LogWarning(device.name);
        // }
    }
}

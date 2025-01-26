using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
    //     InputSystem.onDeviceChange += DeviceChange;
    // }

    // void OnDisable()
    // {
    //     InputSystem.onDeviceChange -= DeviceChange;
    // }

    void DeviceChange(InputDevice device, InputDeviceChange change)
    {
        Debug.LogWarning(device.name);
    }
}

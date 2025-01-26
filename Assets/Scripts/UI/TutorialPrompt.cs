using TMPro;
using UnityEngine;

public class TutorialPrompt : MonoBehaviour
{
    [Multiline]
    public string keyboardTutorial, controllerTutorial;

    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        SetKeboardControls();
    }

    public void SetKeboardControls() => text.text = keyboardTutorial;
    public void SetControllerControls() => text.text = controllerTutorial;
}

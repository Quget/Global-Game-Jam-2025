using UnityEngine;
using UnityEngine.UI;

public class DownscaleCamera : MonoBehaviour
{
    public Vector2Int resolution = new Vector2Int(240, 135);

    void Start()
    {
        RenderTexture pixelTexture = CreatePixelCamera();
        SetupCanvas(pixelTexture);
    }

    RenderTexture CreatePixelCamera()
    {
        GameObject cameraGameObject = new GameObject("PixelCamera");
        cameraGameObject.transform.SetParent(Camera.main.transform);
        cameraGameObject.transform.localPosition = Vector3.zero;
        cameraGameObject.transform.localEulerAngles = Vector3.zero;

        Camera pixelCamera = cameraGameObject.AddComponent<Camera>();

        pixelCamera.fieldOfView = Camera.main.fieldOfView;
        pixelCamera.farClipPlane = Camera.main.farClipPlane;
        pixelCamera.nearClipPlane = Camera.main.nearClipPlane;

        RenderTexture renderTexture = new RenderTexture(resolution.x, resolution.y, 24);
        renderTexture.filterMode = FilterMode.Point;

        return pixelCamera.targetTexture = renderTexture;
    }

    void SetupCanvas(RenderTexture pixelTexture)
    {
        Canvas canvas = new GameObject("PixelCanvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.vertexColorAlwaysGammaSpace = true;

        RawImage renderImage = new GameObject("Pixel Result").AddComponent<RawImage>();
        renderImage.transform.SetParent(canvas.transform);
        renderImage.transform.localPosition = Vector3.zero;

        renderImage.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

        renderImage.texture = pixelTexture;
    }
}

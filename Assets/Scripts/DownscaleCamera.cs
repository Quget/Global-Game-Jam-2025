using UnityEngine;
using UnityEngine.UI;

public class DownscaleCamera : MonoBehaviour
{
    public Vector2Int resolution = new Vector2Int(240, 135);
    public Material backgroundMaterial;


    private Canvas canvas;
    private Image backgroundImage;
    private RawImage renderImage;


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

        RenderTexture renderTexture = new RenderTexture(resolution.x, resolution.y, 8);
        renderTexture.filterMode = FilterMode.Point;

        return pixelCamera.targetTexture = renderTexture;
    }

	private void LateUpdate()
	{ 
        if(backgroundImage != null)
        {
            if (backgroundImage.rectTransform.sizeDelta.x != Camera.main.pixelWidth ||
				backgroundImage.rectTransform.sizeDelta.y != Camera.main.pixelHeight)
            {
                UpdateBackGroundBarsSize();
                UpdateRenderSize();
			}
        }
	}

	private void UpdateBackGroundBarsSize()
    {
		backgroundImage.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
	}

    private void UpdateRenderSize()
    {
		renderImage.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

		renderImage.transform.localScale = new Vector3(((float)resolution.x / (float)resolution.y) / ((float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight), 1f, 1f);
	}
    void SetupCanvas(RenderTexture pixelTexture)
    {
        canvas = new GameObject("PixelCanvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.vertexColorAlwaysGammaSpace = true;

        backgroundImage = new GameObject("Background Bars").AddComponent<Image>();
		backgroundImage.transform.SetParent(canvas.transform);
        backgroundImage.transform.localPosition = Vector3.zero;
		// backgroundImage.color = Color.black;
		backgroundImage.material = backgroundMaterial;
		UpdateBackGroundBarsSize();

		renderImage = new GameObject("Pixel Result").AddComponent<RawImage>();
        renderImage.transform.SetParent(canvas.transform);
        renderImage.transform.localPosition = Vector3.zero;

        UpdateRenderSize();

		renderImage.texture = pixelTexture;
    }
}

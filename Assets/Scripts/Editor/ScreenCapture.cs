using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ScreenCapture : MonoBehaviour
{
	public static int datanumber = 0;
	Texture2D screenCapture;
	Sprite photo;
	public List<PhotoSO> photoSOs = new();

	public KeyCode[] screenCaptureKeys;
	public KeyCode[] keyModifiers;

	public int minimumWidth = 1684;
	public int minimumHeight = 2384;
	public string directory = "./";

	public Camera targetCamera;
	public float viewPortWidth = 1.0f;
	public float viewPortHeigh = 1.0f;

	public enum DepthBuffer { DEPTH_0=0, DEPTH_16=16, DEPTH_24=24 };
	public DepthBuffer depthBuffer = DepthBuffer.DEPTH_24;

	public enum Format { PNG, JPG };
	public Format format = Format.PNG;

	void Reset ()
	{
		screenCaptureKeys = new KeyCode[]{ KeyCode.P };
		keyModifiers = new KeyCode[] { KeyCode.LeftShift, KeyCode.RightShift };
	}

	void Update ()
	{
		if (keyModifiers.Length > 0) {
			bool isModifierPressed = false;
			foreach (KeyCode keyCode in keyModifiers) {
				if (Input.GetKey(keyCode)) {
					isModifierPressed = true;
					break;
				}
			}
			if (!isModifierPressed) { return; }
		}

		foreach (KeyCode keyCode in screenCaptureKeys) {
			if (Input.GetKeyDown(keyCode)) {
				if (targetCamera != null) {
					TakeScreenShotWithCamera();
				} else {
					Debug.Log("target camera was not specified.", this);
					//TakeScreenShot();
				}
			}
		}
	}

	public void TakeScreenShot ()
	{
		float rw = (float)minimumWidth / Screen.width;
		float rh = (float)minimumHeight / Screen.height;
		int scale = (int)Mathf.Ceil(Mathf.Max(rw, rh));

		string path = directory + System.DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".png"; // jpeg was not supported

		UnityEngine.ScreenCapture.CaptureScreenshot(path, scale);
		Debug.Log(string.Format("screen shot : path = {0}, scale = {1} (screen = {2}, {3})",
			path, scale, Screen.width, Screen.height), this);
	}

	public void TakeScreenShotWithCamera ()
	{
		int vw = (int)(Screen.width * viewPortWidth);
		int vh = (int)(Screen.height * viewPortHeigh);

		float rw = (float)minimumWidth / vw;
		float rh = (float)minimumHeight / vh;
		float scale = Mathf.Max(rw, rh);

		int tw = (int)Mathf.Ceil(vw * scale);
		int th = (int)Mathf.Ceil(vh * scale);
		RenderTexture renderTexture = RenderTexture.GetTemporary(tw, th, (int)depthBuffer, RenderTextureFormat.ARGB32);
		//photoSOs[datanumber].SetTexture(new Texture2D(tw, th, TextureFormat.ARGB32, false));
		
		RenderTexture oldTargetTexture = targetCamera.targetTexture;
		RenderTexture oldActiveTexture = RenderTexture.active;

		targetCamera.targetTexture = renderTexture;
		targetCamera.Render();

		RenderTexture.active = renderTexture;
		//photoSOs[datanumber].GetTexture2D().ReadPixels(new Rect(0, 0, tw, th), 0, 0);
		//photoSOs[datanumber].GetTexture2D().Apply();
		string path = directory + System.DateTime.Now.ToString(datanumber.ToString()) + (format == Format.PNG ? ".png" : ".jpg");

	    //System.IO.File.WriteAllBytes(path, photoSOs[datanumber].GetTexture2D().EncodeToPNG());
		AssetDatabase.Refresh();
		AssetDatabase.ImportAsset(path);
		TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
		importer.textureType = TextureImporterType.Sprite;
		AssetDatabase.WriteImportSettingsIfDirty(path);
		//photo = Sprite.Create(photoSOs[datanumber].GetTexture2D(), new Rect(0.0f, 0.0f, photoSOs[datanumber].GetTexture2D().width, photoSOs[datanumber].GetTexture2D().height), new Vector2(0.5f, 0.5f), 100.0f);
		//photoSOs[datanumber].SetScreenShot(photo);
		datanumber++;

		/*RenderTexture.active = oldActiveTexture;
        targetCamera.targetTexture = oldTargetTexture;
        //targetCamera.Render();

        RenderTexture.ReleaseTemporary(renderTexture);

        
        Debug.Log(string.Format("screen shot with camera : path = {0}, scale = {1:F2} (view = {2}, {3})",
            path, scale, vw, vh), this);*/


	}
}

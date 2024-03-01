using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTaker : MonoBehaviour
{
    public static int datanumber = 0;
    Texture2D screenCapture;
    Sprite photo;
    public List<PhotoSO> photoSOs = new();


    // Start is called before the first frame update

    // Update is called once per frame
    public void CapturePhoto()
    {
        GetComponent<ScreenCapture>().TakeScreenShotWithCamera();
        /*photo = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoSOs[datanumber].SetScreenShot(photo);
        datanumber++;*/
    }
}
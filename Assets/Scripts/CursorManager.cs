using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Sprite crosshairSprite;
    public Transform crosshairLimit;
    public float sensitivity = 1f;
    private Camera cam;
    private Image image;
    [SerializeField]
    private float crosshairTopOffset = 10;
    private void Start()
    {
        cam = GetComponentInParent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        image = GetComponent<Image>();
        SpriteChange(crosshairSprite);
    }

    void Update()
    {
        if (Input.GetAxis("Mouse Y") != 0)
        {
            CrosshairMovement();
        }
    }

    public void SpriteChange(Sprite _newSprite)
    {
        image.sprite = _newSprite;
    }

    public void CrosshairMovement()
    {
        Vector3 nextPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Input.GetAxis("Mouse Y") * sensitivity, transform.localPosition.z);
        float crosshairMaxY = cam.ScreenToWorldPoint(new Vector3(nextPosition.x, Screen.height, nextPosition.z)).y - image.sprite.bounds.size.y * 0.5f;
        float crosshairMinY = cam.ScreenToWorldPoint(crosshairLimit.localPosition).y;
        transform.localPosition = new Vector3(nextPosition.x, Mathf.Clamp(nextPosition.y, -Screen.height * 0.28f, (Screen.height - image.sprite.bounds.size.y - crosshairTopOffset) * sensitivity), nextPosition.z);
    }
}

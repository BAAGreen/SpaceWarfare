using UnityEngine.UI;
using UnityEngine;

public class MobileControllers : MonoBehaviour {

    static public bool isPressed = false;

    private Vector2 inputVector;

    private Touch touch;
    private Vector2 joystickCenterPoint;
    private Image joystick;
    private Image joystickKnob;

    private void Start()
    {
        Input.multiTouchEnabled = true;
        joystick = GetComponent<Image>();
        joystickKnob = transform.GetChild(0).GetComponent<Image>();

        joystick.enabled = false;
        joystickKnob.enabled = false;
    }

    public void Update()
    {
        InvisibleJoystick();

        if (!isPressed)
        {
            inputVector = Vector2.Lerp(inputVector, Vector2.zero, 5 * Time.deltaTime);
            if (Mathf.Abs(inputVector.x) < 0.1f)
            {
                inputVector.x = 0.0f;
            }
            if (Mathf.Abs(inputVector.y) < 0.1f)
            {
                inputVector.y = 0.0f;
            }
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }

    private void InvisibleJoystick()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if(Input.GetTouch(i).position.x < Screen.width / 2) touch = Input.GetTouch(i);
            }
            if (touch.phase == TouchPhase.Began)
            {
                joystickCenterPoint = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                joystick.enabled = true;
                joystickKnob.enabled = true;
                joystick.rectTransform.position = new Vector2(joystickCenterPoint.x, joystickCenterPoint.y);
                joystickKnob.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystick.rectTransform.sizeDelta.x / 2), inputVector.y * (joystick.rectTransform.sizeDelta.y / 2));
                inputVector.x = (touch.position.x - joystickCenterPoint.x) * 0.01f;
                inputVector.y = (touch.position.y - joystickCenterPoint.y) * 0.01f;
                isPressed = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isPressed = false;
                joystick.enabled = false;
                joystickKnob.enabled = false;
            }

            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        }
    }
}

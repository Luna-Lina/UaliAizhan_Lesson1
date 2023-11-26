using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image JoystickBG;
    [SerializeField]
    private Image Joystick;
    private Vector2 InputVetor; //получение координат джостика

    private void Start()
    {
        JoystickBG = GetComponent<Image>();
        Joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputVetor = Vector2.zero;
        Joystick.rectTransform.anchoredPosition = Vector2.zero;//возврат джостика в центр
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / JoystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джостик
            pos.y = (pos.y / JoystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джостик

            InputVetor = new Vector2(pos.x, pos.y);//установка точных координат из касания
            InputVetor = (InputVetor.magnitude > 1.0f) ? InputVetor.normalized : InputVetor;

            Joystick.rectTransform.anchoredPosition = new Vector2(InputVetor.x * (JoystickBG.rectTransform.sizeDelta.x / 2), InputVetor.y * (JoystickBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public float Horizontal()
    {
        if (InputVetor.x != 0) return InputVetor.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (InputVetor.y != 0) return InputVetor.y;
        else return Input.GetAxis("Vertical");
    }
}

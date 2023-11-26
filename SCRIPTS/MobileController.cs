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
    private Vector2 InputVetor; //��������� ��������� ��������

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
        Joystick.rectTransform.anchoredPosition = Vector2.zero;//������� �������� � �����
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / JoystickBG.rectTransform.sizeDelta.x);//��������� ��������� ������� ������� �� �������
            pos.y = (pos.y / JoystickBG.rectTransform.sizeDelta.x);//��������� ��������� ������� ������� �� �������

            InputVetor = new Vector2(pos.x, pos.y);//��������� ������ ��������� �� �������
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

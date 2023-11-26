using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    //Основные параметры
    public float speedMove; //Скорость персонажа
    public float jumpPower; //Сила прижка

    //Параметры геймплейя для персонажа
    private float gravityForce; //Гравитация персонажа
    private Vector3 moveVector; //Направление движения персонажа

    //Ссылки на компоненты
    private CharacterController ch_controller;
    private Animator ch_animator;
    private MobileController mContr;

    private void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        
        GameObject joystickObject = GameObject.FindGameObjectWithTag("Joystick");

        if (joystickObject != null)
        {
            mContr = joystickObject.GetComponent<MobileController>();
        }
        else
        {
            Debug.LogError("Joystick object not found!");
        }
        //mContr = GameObject.FindGameObjectsWithTag("Joystick").GetComponent<MobileController>();
    }

    private void Update()
    {
        CharacterMove();
        GamingGravity();
    }

    //Метод перемещения персонажа
    private void CharacterMove()
    {
        //Перемещение по поверхности
        if (ch_controller.isGrounded)
        {
            ch_animator.ResetTrigger("Jump");

            moveVector = Vector3.zero;
            moveVector.x = mContr.Horizontal() * speedMove;
            moveVector.z = mContr.Vertical() * speedMove;

            //Анимация передвижения персонажа
            if (moveVector.x != 0 || moveVector.z != 0) ch_animator.SetBool("Move", true);
            else ch_animator.SetBool("Move", false);

             if(Vector3.Angle(Vector3.forward, moveVector)> 1f|| Vector3.Angle(Vector3.forward, moveVector) ==0)
             {
                 Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                  transform.rotation = Quaternion.LookRotation(direct);
             }
        }
        


        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime); //Метод передвижения по направлению
    }

    //Метод гравитации
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded)
        {
            gravityForce = jumpPower;
            ch_animator.SetTrigger("Jump");
        }
    }

}

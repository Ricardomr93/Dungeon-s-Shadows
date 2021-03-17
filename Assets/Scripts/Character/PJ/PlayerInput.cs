using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


//indicamos que este script debe ejecutarse el primero
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public Joystick joystick;
    public bool plataformaAndroid;

    //HideInInspector para que aunque sean publicas pero no se muestren en el inspector
    [HideInInspector] public float horizontal;
    [HideInInspector] public bool jumpHeld;
    [HideInInspector] public bool jumPressed;
    [HideInInspector] public bool attackPressed;
    private bool readyToClear;


    void Update()
    {
        ClearInput();

        if (GameManager.IsGameOver() || GameManager.PlayerDied() || GameManager.IsRespawn()) return;

        //Procesa las teclas
        ProcessInputs();
        //Procesa los inputs movil
        ProcessTouchInputs();
        //recortar los valores entre -1 y 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }
    private void FixedUpdate()
    {
        //hacer que se reseteen los datos de las teclas
        readyToClear = true;
    }
    private void ProcessTouchInputs()
    {
        //TODO ->
    }

    private void ClearInput()
    {
        //si no se han usado esos datos no se ejecuta
        if (!readyToClear) return;

        //resetar los datos
        horizontal = 0f;
        jumPressed = false;
        jumpHeld = false;
        attackPressed = false;
        readyToClear = false;
    }
    private void ProcessInputs()
    {

        if (plataformaAndroid)
        {
            //acumular horizontal axis inputn
            horizontal += CrossPlatformInputManager.GetAxis("Horizontal");

            //Acumular botones input para no perder entre fotogramas la pulsacion
            jumPressed = jumPressed || CrossPlatformInputManager.GetButtonDown("Jump");
            jumpHeld = jumpHeld || CrossPlatformInputManager.GetButtonDown("Jump");
            attackPressed = attackPressed || CrossPlatformInputManager.GetButtonDown("Fire1");

        } else
        {
            //acumular horizontal axis inputn
            horizontal += Input.GetAxis("Horizontal");

            //Acumular botones input para no perder entre fotogramas la pulsacion
            jumPressed = jumPressed || Input.GetButtonDown("Jump");
            jumpHeld = jumpHeld || Input.GetButtonDown("Jump");
            attackPressed = attackPressed || Input.GetButtonDown("Fire1");
        }
    }
}

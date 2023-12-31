using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PaddleInputs;

public class InputPlayer : MonoBehaviour
{
    [SerializeField]
    private PaddleInput _playerinput;
    [HideInInspector]public event Action Paddlerightpressed;
    [HideInInspector]public event Action Paddleleftpressed;
    [HideInInspector] public event Action Paddlerightreleased;
    [HideInInspector] public event Action Paddleleftreleased;
    [HideInInspector] public event Action<bool> Launchpressed,Launchcancelled;



    private void Awake()
    {
        _playerinput = new PaddleInput();
        _playerinput.Game.Enable();
       
    }
    private void OnEnable()
    {
        _playerinput.Game.PaddleLeft.performed += PaddleLeftPressed;
        _playerinput.Game.PaddleRight.performed += PaddleRightPressed;
        _playerinput.Game.PaddleLeft.canceled += PaddleLeftReleased;
        _playerinput.Game.PaddleRight.canceled += PaddleRightReleased;
        _playerinput.Game.LauncBall.performed += LaunchPressed;
        _playerinput.Game.LauncBall.canceled += LaunchCancelled;
    }

    private void OnDisable ()
    {
        _playerinput.Game.PaddleLeft.performed -= PaddleLeftPressed;
        _playerinput.Game.PaddleRight.performed -= PaddleRightPressed;
        _playerinput.Game.PaddleLeft.canceled -= PaddleLeftReleased;
        _playerinput.Game.PaddleRight.canceled -= PaddleRightReleased;
        _playerinput.Game.LauncBall.performed -= LaunchPressed;
        _playerinput.Game.LauncBall.canceled -= LaunchCancelled;

    }


    private void PaddleLeftPressed(InputAction.CallbackContext obj)
    {
        Paddleleftpressed?.Invoke();
    }



    private void PaddleRightPressed(InputAction.CallbackContext obj)
    {
        Paddlerightpressed?.Invoke();
    }

    private void PaddleLeftReleased(InputAction.CallbackContext obj)
    {
        Paddleleftreleased?.Invoke();
    }

    private void PaddleRightReleased(InputAction.CallbackContext obj)
    {
        Paddlerightreleased?.Invoke();
    }

    private void LaunchPressed(InputAction.CallbackContext obj)
    {
        Launchpressed?.Invoke(true);
    }

    private void LaunchCancelled(InputAction.CallbackContext obj)
    {
        Launchcancelled?.Invoke(false);
    }
}

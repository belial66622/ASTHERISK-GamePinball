using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _springSpeed , _modifier;
    float _defaultSpringSpeed, _defaultModifier, _targetPressed, _targetReleased;
    [SerializeField] private InputPlayer _input;
    [SerializeField] private GameObject _paddleLeft;
    [SerializeField] private GameObject _paddleright;
    private HingeJoint _hingeRight, _hingeLeft;



    private void OnEnable()
    {
        _input.Paddleleftpressed+= PaddleLeftPressed;
        _input.Paddlerightpressed += PaddleRightPressed;
        _input.Paddleleftreleased += PaddleLeftReleased;
        _input.Paddlerightreleased += PaddleRightReleased;
    }


    private void OnDisable()
    {
        _input.Paddleleftpressed -= PaddleLeftPressed;
        _input.Paddlerightpressed -= PaddleRightPressed;
        _input.Paddleleftreleased -= PaddleLeftReleased;
        _input.Paddlerightreleased -= PaddleRightReleased;

    }

    private void Start()
    {
        _hingeLeft = _paddleLeft.GetComponent<HingeJoint>();
        _hingeRight = _paddleright.GetComponent<HingeJoint>();
        _defaultModifier = _modifier;
        _targetPressed = _hingeLeft.limits.max;
        _targetReleased = _hingeRight.limits.min;
    }

    void PaddleLeftPressed ()
    {
        // mengambil spring dari component Hinge joint
        JointSpring jointSpring = _hingeLeft.spring;

        // mengubah value spring saat input ditekan
        jointSpring.targetPosition = _targetPressed;

//        jointSpring.spring = _springSpeed * _modifier;


        // mengubah spring pada Hinge Joint dengan value yang sudah di ubah
        _hingeLeft.spring = jointSpring;

        Debug.Log("kiri");
    }


    void PaddleRightPressed () 
    {
        JointSpring jointSpring = _hingeRight.spring;

        // mengubah value spring saat input ditekan
        jointSpring.targetPosition = _targetPressed;

//        jointSpring.spring = _springSpeed * _modifier;


        // mengubah spring pada Hinge Joint dengan value yang sudah di ubah
        _hingeRight.spring = jointSpring;
        Debug.Log("Kanan");
    }



    void PaddleLeftReleased()
    {
        // mengambil spring dari component Hinge joint
        JointSpring jointSpring = _hingeLeft.spring;

        // mengubah value spring saat input dilepas

        jointSpring.targetPosition = _targetReleased;
//        jointSpring.spring = 0;


        // mengubah spring pada Hinge Joint dengan value yang sudah di ubah
        _hingeLeft.spring = jointSpring;

        Debug.Log("kiri");
    }


    void PaddleRightReleased()
    {
        JointSpring jointSpring = _hingeRight.spring;

        // mengubah value spring saat input ditekan

        jointSpring.targetPosition = _targetReleased;
//        jointSpring.spring = 0;


        // mengubah spring pada Hinge Joint dengan value yang sudah di ubah
        _hingeRight.spring = jointSpring;
        Debug.Log("Kanan");
    }
}

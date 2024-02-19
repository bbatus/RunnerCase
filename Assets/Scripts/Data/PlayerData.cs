using UnityEngine;
using System;
[Serializable]
public class PlayerData //data class'tan playercontroller'a çekicez
//Public degiskenleri ileride get set ile encapsule et!!
{
    [Space(20)]
    [Header("Forward Move")]
    [SerializeField] private float speed = 4f;
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, minSpeed, maxSpeed);
    }

    [Space(20)]
    [Header("Drag Mechanism")]
    [SerializeField] private float dragSpeed = 0.0075f * 3.34f;
    public float DragSpeed
    {
        get => dragSpeed;
        set => dragSpeed = Mathf.Clamp(value, minDragSpeed, maxDragSpeed);
    }

    public bool IsDrag { get; set; } = true;

    [SerializeField] private float maxPosX; //Drag en fazla ne kadar yapilabilir
    public float MaxPosX
    {
        get => maxPosX;
        set => maxPosX = value; //Inspectordan yap
    }
    [SerializeField] private float minPosX; //Drag en az ne kadar yapilabilir
    public float MinPosX
    {
        get => minPosX;
        set => minPosX = value; //Inspectordan yap
    }
    [Space(20)]
    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    public float JumpForce
    {
        get => jumpForce;
        set => jumpForce = Mathf.Clamp(value, minJumpForce, maxJumpForce);
    }

    [Space(20)]
    [Header("Crouch")]
    [SerializeField] private float crouchScaleY = 0.5f;
    public float CrouchScaleY
    {
        get => crouchScaleY;
        set => crouchScaleY = Mathf.Clamp(value, minCrouchScaleY, maxCrouchScaleY);
    }

    private float mouseX = 0f;
    public float MouseX
    {
        get => mouseX;
        set => mouseX = Mathf.Clamp(value, MinPosX, MaxPosX);
    }
    [Space(20)]
    [Header("Constans")]
    public const float minSpeed = 1f, maxSpeed = 10f;
    private const float minDragSpeed = 0.1f, maxDragSpeed = 1f;
    private const float minJumpForce = 1f, maxJumpForce = 10f;
    private const float minCrouchScaleY = 0.3f, maxCrouchScaleY = 1f;

}

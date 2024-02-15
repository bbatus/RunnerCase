using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    #region References
    [SerializeField] private PlayerData playerData;
    public LevelState levelState;
    public static PlayerController instance { get; private set; }
    private Rigidbody rb;
    private Vector3 originalScale;
    #endregion

    #region Controllers
    private float playerStartSpeed = 0;
    private List<float> mouseXList;
    private bool isCrouching = false;
    [SerializeField] public bool IsGrounded = true;
    #endregion

    private void Awake()
    {
        Debug.Log("Awake basladi");
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        FindComponents();
    }

    private void FindComponents()
    {
        rb = GetComponentInChildren<Rigidbody>();
        originalScale = transform.localScale;
        mouseXList = new List<float>();
        Debug.Log("Componentler bulundu");
    }
    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    private void FixedUpdate()
    {
        if (UIManager.instance.levelState == LevelState.Playing)
        {
            Move();
            Drag();
            CheckJump();
            CheckCrouch();
        }

        //Jump();
        //Crouch(isCrouching);
        //Debug.Log("Fixed update girdi");
    }
    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded)
        {
            Debug.Log("ziplama inputu aldi");
            Jump();
        }
    }
    private void CheckCrouch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouching = true;
            Debug.Log("egilme inputu aldi");
            Crouch(isCrouching);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;
            Crouch(isCrouching);
            Debug.Log("egilme birakti");
        }
    }

    private void Move() //ileri gitme icin rigidbody kullanmasak da olur
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerData.Speed);
        //Debug.Log("Move calisiyor");
    }

    public void SpeedEdit(float _time)
    {
        DOTween.To(() => playerData.Speed, x => playerData.Speed = x, playerStartSpeed, _time); //Hızı bir anda yükseltmemek icin
    }

    private void Drag()
    {
        // Debug.Log("Drag girdi");
        if (!playerData.IsDrag)
            return;


#if UNITY_EDITOR
        float horizontalInput = Input.GetAxis("Horizontal");
        playerData.MouseX = Mathf.Clamp(playerData.MouseX + horizontalInput * playerData.DragSpeed, playerData.MinPosX, playerData.MaxPosX);

#else
    if (Input.touchCount > 0)
    {
        _mouseX = Mathf.Clamp(_mouseX + Mathf.Clamp(Input.touches[0].deltaPosition.x, -35, 35) * playerData.DragSpeed * 2, playerData.MinPosX, playerData.MaxPosX);
    }
#endif

        UpdateMouseXList(playerData.MouseX);
        UpdatePosition();

    }

    private void UpdateMouseXList(float mouseX)
    {
        mouseXList.Insert(0, playerData.MouseX);
        if (mouseXList.Count > 20)
            mouseXList.RemoveAt(mouseXList.Count - 1);
    }

    private void UpdatePosition()
    {
        transform.position = new Vector3(Mathf.Clamp(playerData.MouseX, playerData.MinPosX, playerData.MaxPosX), transform.position.y, transform.position.z);
    }

    private void Jump()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody yok");
            return;
        }

        Debug.Log("Zipladi");
        rb.AddForce(Vector3.up * playerData.JumpForce, ForceMode.Impulse);
        IsGrounded = false;
    }

    private void Crouch(bool isCrouching)
    {
        Debug.Log("egildi");
        if (isCrouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, playerData.CrouchScaleY, transform.localScale.z);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

}

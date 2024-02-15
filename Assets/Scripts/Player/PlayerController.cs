using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Space(20)]
    [Header("References")]
    public static PlayerController instance = null;
    [SerializeField] private float playerSpeed = 4f;
    [SerializeField] private float dragSpeed = 0.0075f * 3.34f;
    [SerializeField] public bool isDrag = true;
    [SerializeField] private float maxPosX;
    [SerializeField] private float minPosX;
    private float playerStartSpeed = 0;
    public float _mouseX;
    [SerializeField]
    List<float> mouseXList;

    [Space(20)]
    [Header("References")]
    public float jumpForce = 5f;
    public float crouchScaleY = 0.5f; // Karakterin eğilme sırasında Y ekseni boyunca ölçeklendirme faktörü
    public bool isGrounded = true; // Karakterin yerde olup olmadığını kontrol etmek için
    private Vector3 originalScale; // Karakterin orijinal ölçeğini saklamak için
    private Rigidbody rb;
    private bool isCrouching = false;
    public LevelState levelState;
    private void Awake()
    {
        Debug.Log("Awake basladi");
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        FindComponents();
    }

    private void FindComponents()
    {
        rb = GetComponentInChildren<Rigidbody>();
        originalScale = transform.localScale;
        Debug.Log("Componentler bulundu");
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
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            Jump();
            Debug.Log("ziplama inputu aldi");
        }
    }
    private void CheckCrouch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouching = true;
            Crouch(isCrouching);
            Debug.Log("egilme inputu aldi");
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
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
        //Debug.Log("Move calisiyor");
    }

    public void SpeedEdit(float _time)
    {
        DOTween.To(() => playerSpeed, x => playerSpeed = x, playerStartSpeed, _time); //Hızı bir anda yükseltmemek icin
    }

    private void Drag()
    {
        // Debug.Log("Drag girdi");
        if (!isDrag)
            return;


#if UNITY_EDITOR
        float horizontalInput = Input.GetAxis("Horizontal");
        _mouseX = Mathf.Clamp(_mouseX + horizontalInput * dragSpeed, minPosX, maxPosX);

#else
    if (Input.touchCount > 0)
    {
        _mouseX = Mathf.Clamp(_mouseX + Mathf.Clamp(Input.touches[0].deltaPosition.x, -35, 35) * dragSpeed * 2, minPosX, maxPosX);
    }
#endif

        UpdateMouseXList(_mouseX);
        UpdatePosition();

    }

    private void UpdateMouseXList(float mouseX)
    {
        mouseXList.Insert(0, mouseX);
        if (mouseXList.Count > 20)
            mouseXList.RemoveAt(mouseXList.Count - 1);
    }

    private void UpdatePosition()
    {
        transform.position = new Vector3(Mathf.Clamp(_mouseX, minPosX, maxPosX), transform.position.y, transform.position.z);
    }

    private void Jump()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody yok");
            return;
        }

        Debug.Log("Zipladi");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void Crouch(bool isCrouching)
    {
        Debug.Log("egildi");
        // Eğilme mantığı buraya eklenecek
        // Örneğin, karakterin collider boyutunu azaltarak
        if (isCrouching)
        {
            // Eğilme başladığında karakterin ölçeğini azalt
            transform.localScale = new Vector3(transform.localScale.x, crouchScaleY, transform.localScale.z);
        }
        else
        {
            // Eğilme bittiğinde karakterin ölçeğini orijinaline döndür
            transform.localScale = originalScale;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7) 
        {
            isGrounded = true;
        }
    }


}

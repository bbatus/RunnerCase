using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance = null;
    [SerializeField] private float playerSpeed = 4f;
    [SerializeField] private float dragSpeed = 0.0075f * 3.34f;
    [SerializeField] public bool isDrag = true;
    [SerializeField] private float maxPosX;
    [SerializeField] private float minPosX;
    public float _mouseX;
    [SerializeField]
    List<float> mouseXList;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Move();
        Drag();
    }

    private void Move() //ileri gitme icin rigidbody kullanmasak da olur
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
    }

    private void Drag()
    {
        if (!isDrag)
            return;

#if UNITY_EDITOR
        _mouseX = Mathf.Clamp((_mouseX + Input.GetAxis("Mouse X") * dragSpeed * 12.5f), minPosX, maxPosX);
#else
        if (Input.touchCount > 0)
        {

            _mouseX = Mathf.Clamp((_mouseX + Mathf.Clamp(Input.touches[0].deltaPosition.x, -35, 35) * dragSpeed * 2), minPosX, maxPosX);//, DragIncrementSpeed * Time.fixedDeltaTime);
        }
#endif

        mouseXList.Insert(0, _mouseX);

        if (mouseXList.Count > 20)
            mouseXList.RemoveAt(mouseXList.Count - 1);

        transform.position = new Vector3(Mathf.Clamp(_mouseX, minPosX, maxPosX), transform.position.y, transform.position.z);

    }

}

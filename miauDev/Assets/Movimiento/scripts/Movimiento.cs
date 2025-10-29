using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Cámara")]
    public List<Camera> cameras;       // Lista de cámaras
    public Transform cameraPivot;      // Objeto que actúa como "cabeza"
    public float mouseSensitivity = 100f;
    public float cameraSmoothSpeed = 10f; // Suavidad al mover la cámara
    private int currentCameraIndex = 0;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Bloquear el cursor en pantalla
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // Activar solo la primera cámara
        for (int i = 0; i < cameras.Count; i++)
            cameras[i].gameObject.SetActive(i == 0);
    }

    void Update()
    {
        MovimientoJugador();
        MovimientoCamara();

        // Cambiar cámara con H
        if (Input.GetKeyDown(KeyCode.H))
            CambiarCamara();
    }

    void MovimientoJugador()
    {
        // Verificar si está en el suelo
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Movimiento WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Correr con Shift + W
        float speed = (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) ? runSpeed : walkSpeed;
        controller.Move(move * speed * Time.deltaTime);

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void MovimientoCamara()
    {
        if (cameraPivot == null || cameras.Count == 0) return;

        // Movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación vertical (mirar arriba/abajo)
        xRotation -= mouseY;

        // Limitar rotación vertical para evitar giros antinaturales
        // Puedes ajustar los valores (-70f, 70f) según lo que se sienta más natural
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        // Aplicar rotación suavizada a la cabeza/cámara
        Quaternion targetRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cameraPivot.localRotation = Quaternion.Lerp(
            cameraPivot.localRotation,
            targetRotation,
            cameraSmoothSpeed * Time.deltaTime
        );

        // Rotación horizontal del cuerpo
        transform.Rotate(Vector3.up * mouseX);
    }

    void CambiarCamara()
    {
        if (cameras.Count == 0) return;

        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Count;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}

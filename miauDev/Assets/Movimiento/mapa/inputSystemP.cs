using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class inputSystemP : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Cámara")]
    public List<Camera> cameras;
    public Transform cameraPivot;
    public float mouseSensitivity = 100f;
    public float cameraSmoothSpeed = 10f;

    [Header("Vida del Jugador")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Disparo estilo Doom")]
    public float damage = 25f;
    public float range = 50f;
    public int maxAmmo = 5;
    public float reloadTime = 1.5f;
    public LineRenderer laserLine;
    public LayerMask hitLayers;

    [Header("UI")]
    public Text vidaTexto;
    public Text balasTexto;
    public GameObject deathScreen;

    private int currentAmmo;
    private bool isReloading = false;
    private bool mostrandoLaser = false;

    private int currentCameraIndex = 0;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    private PlayerControlls controls;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool jumpPressed;
    private bool runPressed;
    private bool shootPressed;
    private bool reloadPressed;

    private Animator animator;

    // ======== CICLO DE VIDA ========
    void Awake()
    {
        controls = new PlayerControlls();
    }

    void OnEnable()
    {
        controls.Enable();

        // Movimiento
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMoveCancel;

        // Mirar
        controls.Player.Look.performed += OnLook;
        controls.Player.Look.canceled += OnLookCancel;

        // Saltar
        controls.Player.Jump.performed += OnJump;
        controls.Player.Jump.canceled += OnJumpCancel;

        // Correr
        controls.Player.Run.performed += OnRun;
        controls.Player.Run.canceled += OnRunCancel;

        // Cambiar cámara
        controls.Player.ChangeCamera.performed += ctx => CambiarCamara();

        // Disparar
        controls.Player.Fire.performed += ctx => shootPressed = true;
        controls.Player.Fire.canceled += ctx => shootPressed = false;

        // Recargar
        controls.Player.Reload.performed += ctx => reloadPressed = true;

        // Panel poema
        controls.Player.TogglePanelPoe.performed += ctx => TogglePoemaPanel();
    }

    void OnDisable()
    {
        // Remover todos los eventos para evitar fugas de memoria
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMoveCancel;
        controls.Player.Look.performed -= OnLook;
        controls.Player.Look.canceled -= OnLookCancel;
        controls.Player.Jump.performed -= OnJump;
        controls.Player.Jump.canceled -= OnJumpCancel;
        controls.Player.Run.performed -= OnRun;
        controls.Player.Run.canceled -= OnRunCancel;
        controls.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        currentAmmo = maxAmmo;
        currentHealth = maxHealth;

        if (laserLine != null)
            laserLine.enabled = false;

        for (int i = 0; i < cameras.Count; i++)
            cameras[i].gameObject.SetActive(i == 0);

        if (deathScreen != null)
            deathScreen.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ActualizarUI();
    }

    void Update()
    {
        if (currentHealth <= 0) return;

        MovimientoJugador();
        MovimientoCamara();
        ControlDisparo();
    }

    // ======== MOVIMIENTO ========
    void MovimientoJugador()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        float speed = (runPressed && moveInput.y > 0) ? runSpeed : walkSpeed;
        controller.Move(move * speed * Time.deltaTime);

        float moveMagnitude = new Vector2(moveInput.x, moveInput.y).magnitude;
        float currentSpeed = animator.GetFloat("Speed");
        float smoothSpeed = Mathf.Lerp(currentSpeed, moveMagnitude, Time.deltaTime * 10f);
        animator.SetFloat("Speed", smoothSpeed);
        animator.SetBool("IsRunning", runPressed);
        animator.SetBool("IsGrounded", isGrounded);

        if (jumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // ======== CÁMARA ========
    void MovimientoCamara()
    {
        if (cameraPivot == null || cameras.Count == 0) return;

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 20f);

        Quaternion targetRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cameraPivot.localRotation = Quaternion.Lerp(cameraPivot.localRotation, targetRotation, cameraSmoothSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up * mouseX);
    }

    // ======== DISPARO ========
    void ControlDisparo()
    {
        if (isReloading) return;

        if (reloadPressed)
        {
            reloadPressed = false;
            if (currentAmmo < maxAmmo)
                StartCoroutine(Recargar());
            return;
        }

        if (shootPressed)
        {
            shootPressed = false;
            Disparar();
        }
    }

    void Disparar()
    {
        if (currentAmmo <= 0)
            return;

        currentAmmo--;

        Camera cam = cameras[currentCameraIndex];
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 endPos = ray.origin + ray.direction * range;

        if (Physics.Raycast(ray, out hit, range, hitLayers))
        {
            endPos = hit.point;
            Vida vida = hit.collider.GetComponent<Vida>();
            if (vida != null)
                vida.TakeDamage(damage);
        }

        StartCoroutine(MostrarLaser(ray.origin, endPos));
        ActualizarUI();
    }

    IEnumerator MostrarLaser(Vector3 start, Vector3 end)
    {
        if (laserLine == null || mostrandoLaser) yield break;

        mostrandoLaser = true;
        laserLine.enabled = true;
        laserLine.SetPosition(0, start);
        laserLine.SetPosition(1, end);
        yield return new WaitForSeconds(0.05f);
        laserLine.enabled = false;
        mostrandoLaser = false;
    }

    IEnumerator Recargar()
    {
        isReloading = true;

        if (balasTexto != null)
            balasTexto.text = "Recargando...";

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        ActualizarUI();
    }

    // ======== VIDA ========
    public void RecibirDaño(float dmg)
    {
        if (currentHealth <= 0) return;

        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (deathScreen != null)
                deathScreen.SetActive(true);
        }

        ActualizarUI();
    }

    // ======== UI ========
    void ActualizarUI()
    {
        if (vidaTexto != null)
            vidaTexto.text = "Vida: " + Mathf.RoundToInt(currentHealth);

        if (balasTexto != null && !isReloading)
            balasTexto.text = $"{currentAmmo}/{maxAmmo}";
    }

    void CambiarCamara()
    {
        if (cameras.Count == 0) return;
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Count;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }

    public void TogglePoemaPanel() { }
    public void Siguiente() { }

    // ======== INPUT CALLBACKS ========
    void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();
    void OnMoveCancel(InputAction.CallbackContext ctx) => moveInput = Vector2.zero;
    void OnLook(InputAction.CallbackContext ctx) => lookInput = ctx.ReadValue<Vector2>();
    void OnLookCancel(InputAction.CallbackContext ctx) => lookInput = Vector2.zero;
    void OnJump(InputAction.CallbackContext ctx) => jumpPressed = true;
    void OnJumpCancel(InputAction.CallbackContext ctx) => jumpPressed = false;
    void OnRun(InputAction.CallbackContext ctx) => runPressed = true;
    void OnRunCancel(InputAction.CallbackContext ctx) => runPressed = false;
}

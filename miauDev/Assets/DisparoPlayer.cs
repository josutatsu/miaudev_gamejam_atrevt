using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisparoPlayer : MonoBehaviour
{
    [Header("Configuración de disparo")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 0.3f;
    public int maxAmmo = 5;
    public float reloadTime = 1.5f;

    [Header("Efectos visuales y sonoros")]
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public AudioSource reloadSound;
    public Camera fpsCam;
    public LineRenderer laserLine;   // <- visible ray
    public float laserDuration = 0.05f; // cuánto dura el rayo visible

    private int currentAmmo;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    // Input System
    private PlayerInput playerInput;
    private InputAction shootAction;
    private InputAction reloadAction;

    void Start()
    {
        currentAmmo = maxAmmo;

        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Fire"];
        reloadAction = playerInput.actions["Reload"];

        // Configurar LineRenderer si no está asignado
        if (laserLine)
        {
            laserLine.enabled = false;
        }
    }

    void Update()
    {
        if (isReloading)
            return;

        if (reloadAction.WasPressedThisFrame())
        {
            StartCoroutine(Reload());
            return;
        }

        if (shootAction.IsPressed() && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Sin balas, recarga con click derecho.");
            return;
        }

        currentAmmo--;

        if (muzzleFlash) muzzleFlash.Play();
        if (shootSound) shootSound.Play();

        RaycastHit hit;
        Vector3 shootOrigin = fpsCam.transform.position;
        Vector3 shootDirection = fpsCam.transform.forward;

        // Activar visual del rayo
        if (laserLine)
        {
            StartCoroutine(ShowLaser(shootOrigin, shootOrigin + shootDirection * range));
        }

        // Disparar el raycast
        if (Physics.Raycast(shootOrigin, shootDirection, out hit, range))
        {
            // Si impacta, actualiza el final del rayo al punto de impacto
            if (laserLine)
            {
                laserLine.SetPosition(1, hit.point);
            }

            Debug.Log("Impactó en: " + hit.transform.name);

            // Aplicar daño si tiene vida
            Vida target = hit.transform.GetComponent<Vida>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    IEnumerator ShowLaser(Vector3 start, Vector3 end)
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, start);
        laserLine.SetPosition(1, end);

        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Recargando...");
        if (reloadSound) reloadSound.Play();

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Recarga completa.");
    }
}

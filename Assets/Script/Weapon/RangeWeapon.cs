using UnityEngine;
using UnityEngine.Pool;

public class RangeWeapon : Weapon
{
    [Header(" Elements ")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Bullet bulletPrefab;

    [Header(" Bullet Pooling ")]
    private ObjectPool<Bullet> bulletPool;

    [Header ("Aiming Mode")]
    [SerializeField] private bool manualMode = false;

    private bool isShootingInputPressed => Input.GetMouseButton(0);
    void Start()
    {
        attackDelay = 1f / attackFrequency;

        bulletPool = new ObjectPool<Bullet>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            manualMode = !manualMode;
            Debug.Log($"Weapon mode switched to: {(manualMode ? "Manual (Mouse Aim & Click)" : "Auto (Closest Enemy & Auto Fire)")}");
        }

        ManageAimingAndShooting();
    }


    private Bullet CreateFunction()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Configure(this);

        return bulletInstance;
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;

        bullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }

    private Vector2 GetMouseWorldPosition()
    {
        // Assumes your Camera.main is set up for 2D.
        // ScreenToWorldPoint converts mouse position (Input.mousePosition) to world coordinates.
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Essential for correct conversion in 3D-projected 2D
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void ManageAimingAndShooting()
    {
        Vector2 targetUpVector = Vector3.up;
        bool shouldAutoShoot = false; // Only true in auto mode

        if (manualMode)
        {
            // --- MANUAL MODE (Mouse Aim) ---
            Vector2 mouseWorldPosition = GetMouseWorldPosition();
            targetUpVector = (mouseWorldPosition - (Vector2)transform.position).normalized;

            // MANAGE MANUAL SHOOTING
            HandleManualShooting();
        }
        else
        {
            // --- AUTO MODE (Closest Enemy Aim) ---
            Enemy closestEnemy = ClosestEnemy();

            if (closestEnemy != null)
            {
                targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
                shouldAutoShoot = true; // Auto-shoot only when an enemy is found
            }

            // MANAGE AUTO SHOOTING
            if (shouldAutoShoot)
            {
                HandleAutoShooting();
            }
            else
            {
                // Optional: reset timer if no enemy to ensure immediate shot upon finding one
                attackTimer = attackDelay;
            }
        }

        // Apply aiming rotation for both modes
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
    }


    // Handles the original auto-shooting logic (renamed from ManageShooting)
    private void HandleAutoShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0f;
            Shoot();
        }
    }

    // Handles the manual (click-based) shooting logic
    private void HandleManualShooting()
    {
        // Check if the fire button is pressed (e.g., left mouse button)
        if (isShootingInputPressed)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                attackTimer = 0f;
                Shoot();
            }
        }
        else
        {
            // Optional: Reset timer for immediate shot on the next click
            attackTimer = attackDelay;
        }
    }

    private void Shoot()
    {
        Bullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(weaponDamage, transform.up);
    }

}

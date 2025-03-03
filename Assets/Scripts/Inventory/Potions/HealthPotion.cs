using UnityEngine;

public class HealthPotion : MonoBehaviour, IPotion
{
    private int healthRecovery = 3;

    [SerializeField] private float minYClampRight = -45f;
    [SerializeField] private float maxYClampRight = 45f;

    [SerializeField] private float maxYClampLeftUp = 45f;
    [SerializeField] private float minYClampLeftDown = 270f;

    public void UsePotion()
    {
        PlayerHelth.Instanse.HealPlayer(healthRecovery);
    }

    public void MouseFollow()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionInWorld.z = 0;

        Vector3 direction = mousePositionInWorld - PlayerControl.Instanse.transform.position;

        float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool flipX = mousePositionInWorld.x < PlayerControl.Instanse.transform.position.x;

        if (flipX)
        {
            angleZ = 180 - angleZ;
            if (angleZ >= 0f && angleZ <= 90f)
            {
                angleZ = Mathf.Clamp(angleZ, 0f, maxYClampLeftUp);
            }
            else if (angleZ <= 360f && angleZ >= 270f)
            {
                angleZ = Mathf.Clamp(angleZ, minYClampLeftDown, 360f);
            }
        }
        else
        {
            angleZ = Mathf.Clamp(angleZ, minYClampRight, maxYClampRight);
        }


        ActiveWeapon.Instanse.transform.rotation = Quaternion.Euler(0f, flipX ? 180 : 0, angleZ);
    }
}

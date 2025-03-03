using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singlton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite fullStamina, emtyStamina;

    private Transform staminaConteiner;

    private int startingStamina = 3;
    private int maxStamina;
    private float staminaRecoveryRate = 5f;

    private const string STAMINA_CONTAINER_STRING = "StaminaConteiner";

    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
        staminaConteiner = GameObject.Find(STAMINA_CONTAINER_STRING).GetComponent<Transform>();
    }

    public void UseStamina()
    {
        CurrentStamina--;

        UpdateStaminaImages();

        StopAllCoroutines();
        StartCoroutine(RefreshStaminaOverTimeRoutin());
    }

    public void RefreshStamina()
    {
        if (CurrentStamina < maxStamina)
        {
            CurrentStamina++;
        }

        UpdateStaminaImages();
    }

    public void RefreshStaminaOnDeath()
    {
        CurrentStamina = startingStamina;

        UpdateStaminaImages();
    }

    private void UpdateStaminaImages()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            Transform staminaObject = staminaConteiner.GetChild(i);
            Image staminaImage = staminaObject.GetComponent<Image>();

            if (i <= CurrentStamina - 1)
            {
                staminaImage.sprite = fullStamina;
            }
            else
            {
                staminaImage.sprite = emtyStamina;
            }
        }
    }

    private IEnumerator RefreshStaminaOverTimeRoutin()
    {
        while (true)
        {
            yield return new WaitForSeconds(staminaRecoveryRate);
            RefreshStamina();
        }
    }
}

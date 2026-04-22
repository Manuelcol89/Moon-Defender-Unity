using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private HealthHandler moonHealthHandler;

    private Image barImage;
    private TextMeshProUGUI scoreLabel;
    private int moonMaxHealth;

    private Coroutine barRoutine;


    private void OnEnable()
    {
        Actions.OnHealthChanged += Object_OnHealthChanged;
    }


    private void OnDisable()
    {
        Actions.OnHealthChanged -= Object_OnHealthChanged;
    }


    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();

        moonMaxHealth = moonHealthHandler.MaxHealth;

        scoreLabel = transform.Find("ScoreLabel").GetComponent<TextMeshProUGUI>();
    }


    private void Start()
    {
        scoreLabel.text = moonHealthHandler.MaxHealth.ToString();
    }


    private void Object_OnHealthChanged(HealthHandler source, int oldHealth, int newHealth)
    {

        if (source!= null && source.CompareTag("Moon"))
        {
            if (barRoutine != null) StopCoroutine(barRoutine);

            barRoutine = StartCoroutine(LoseHealthSmoothly(oldHealth, newHealth));

            scoreLabel.text = newHealth.ToString();
        }

    }

    private IEnumerator LoseHealthSmoothly(int oldHealth, int newHealth)
    {
        float newHealthNormalized = (float)newHealth / moonMaxHealth;
        float smoothSpeed = 0.1f;

        while (!Mathf.Approximately(barImage.fillAmount, newHealthNormalized))
        {
            barImage.fillAmount = Mathf.MoveTowards(barImage.fillAmount, newHealthNormalized, smoothSpeed * Time.deltaTime);
            yield return null;
        }

        barImage.fillAmount = newHealthNormalized;

        barRoutine = null;
    }

}
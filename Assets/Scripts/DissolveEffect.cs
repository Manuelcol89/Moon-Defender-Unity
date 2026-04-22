using UnityEngine;

public class DissolveEffect : MonoBehaviour
{

    private Material dissolveMaterial;

    private float dissolveAmount;
    private float dissolveSpeed;
    private bool _isDissolving;


    private void Awake()
    {
        dissolveMaterial = GetComponent<SpriteRenderer>().material;
    }


    private void Update()
    {
        if (_isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + dissolveSpeed * Time.deltaTime);
            dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        }
        else
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }

    public void StartDissolve(float dissolveSpeed)
    {
        _isDissolving = true;
        this.dissolveSpeed = dissolveSpeed;
    }

    private void OnDestroy()
    {
        Destroy(dissolveMaterial);
    }
}

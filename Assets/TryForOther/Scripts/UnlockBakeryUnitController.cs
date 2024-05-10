using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockBakeryUnitController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bakeryText;
    [SerializeField] private int maxStoredProductCount;
    [SerializeField] private ProductType productType;
    private int storedProductCount;

    [SerializeField] private int UseProductInSecconds = 0;
    [SerializeField] private Transform coinTransform;
    [SerializeField] private GameObject coinGo;
    private float time;

    [SerializeField] private ParticleSystem smokeParticle;

    // Start is called before the first frame update
    void Start()
    {
        DisplayProductCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (storedProductCount > 0)
        {
            time += Time.deltaTime;

            if (time >= UseProductInSecconds)
            {
                time = 0.0f;
                UseProduct();
            }
        }
        
    }

    private void DisplayProductCount()
    {
        bakeryText.text = storedProductCount.ToString() + "/" + maxStoredProductCount.ToString();
        ControlSmokeEffect();
    }

    public ProductType GetNeededProductType()
    {
        return productType;
    }

    public bool StoredProduct()
    {
        if (maxStoredProductCount == storedProductCount)
        {
            return false;
        }

        storedProductCount++;
        DisplayProductCount();
        return true;
    }

    private void UseProduct()
    {
        storedProductCount--;
        DisplayProductCount();
        CreateCoin();
    }

    private void CreateCoin()
    {
        Vector3 position = Random.insideUnitSphere * 1f;
        Vector3 instantiatePos = coinTransform.position + position;

        Instantiate(coinGo, instantiatePos, Quaternion.identity);
    }

    private void ControlSmokeEffect()
    {
        if (storedProductCount == 0)
        {
            if (smokeParticle.isPlaying)
            {
                smokeParticle.Stop();
            }
        }
        else
        {
            if (smokeParticle.isStopped) 
            { 
                smokeParticle.Play();
            }
        }
    }
}

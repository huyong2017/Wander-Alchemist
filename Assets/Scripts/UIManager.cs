using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    // Start is called before the first frame update
    public GameObject Prefabs;
    public GameObject Prefabs2;
    public GameObject FillAmount_fire;
    public GameObject FillAmount_light;
    public GameObject FillAmount_oxygen;
    public GameObject FillAmount_frost;
    public GameObject Img;
    public GameObject OutLight;
    public Image[] Image;
    private Image imgFillAmount_fire;
    private Image imgFillAmount_light;
    private Image imgFillAmount_oxygen;
    private Image imgFillAmount_frost;
    float time = 0;
    int count = 0;
    void Start()
    {
        instance = this;
        Prefabs.GetComponent<ParticleSystem>().Stop();
        imgFillAmount_fire = FillAmount_fire.GetComponent<Image>();
        imgFillAmount_light = FillAmount_light.GetComponent<Image>();
        imgFillAmount_oxygen = FillAmount_oxygen.GetComponent<Image>();
        imgFillAmount_frost = FillAmount_frost.GetComponent<Image>();
        OutLight.SetActive(false);
    }


    // Update is called once per frame
    public void useFire(bool Fire)//喷射火焰 
    {       
        if (Fire)
        {
            if (imgFillAmount_fire.fillAmount > 0f)
            {
                Prefabs.SetActive(true);
                Prefabs.GetComponent<ParticleSystem>().Play();
                time += Time.deltaTime;
                imgFillAmount_fire.fillAmount  -= time / 5;
                imgFillAmount_frost.fillAmount += time / 10;
            }
            if (imgFillAmount_fire.fillAmount <= 0f)
            {            
                Prefabs.SetActive(false);
                Prefabs2.SetActive(true);
                useFrost(true);
            }
                time = 0;
        }
        if (!Fire)
        {
            Prefabs.GetComponent<ParticleSystem>().Stop();
            useFrost(false);
        }
    }
    public void useLight(bool Light)//打开光源
    { 
        if (Light)
        {
            OutLight.SetActive(true);
            if (imgFillAmount_light.fillAmount > 0f)
            {
                time += Time.deltaTime;
                imgFillAmount_light.fillAmount -= time / 20;
            }
            if(imgFillAmount_light.fillAmount <= 0f)
            {
                imgFillAmount_light.fillAmount = 0f;
                OutLight.SetActive(false);
            }
            time = 0;
        }
        if (!Light)
        {
            OutLight.SetActive(false);
        }
    }
    public void useOxygen(bool Oxygen)//释放氧气
    {       
        if (Oxygen)
        {
            if (imgFillAmount_oxygen.fillAmount > 0f)
            {
                time += Time.deltaTime;
                imgFillAmount_oxygen.fillAmount -= time / 5;
            }
            if (imgFillAmount_oxygen.fillAmount <= 0f)
            {
                imgFillAmount_oxygen.fillAmount = 0f;
            }
            time = 0;
        }
    }
    public void useFrost(bool Frost)//释放寒气
    {
        if (Frost)
        {
            if (imgFillAmount_frost.fillAmount > 0f)
            {
                Prefabs2.GetComponent<ParticleSystem>().Play();
                time += Time.deltaTime;
                imgFillAmount_frost.fillAmount -= time / 5;
            }
            if (imgFillAmount_frost.fillAmount <= 0f)
            {
                Prefabs2.SetActive(false);
            }
            time = 0;
        }
        if (!Frost)
        {
            Prefabs2.GetComponent<ParticleSystem>().Stop();
        }
    }
    public void ChangePack(int i)//更换背包
    {
        Img.GetComponent<Image>().sprite = Image[i%3].sprite;
    }
    public void addPower(int i,float t)//增加能量条
    {
        if (i == 0)
        {
            imgFillAmount_fire.fillAmount +=t;
        }
        if (i == 1)
        {
            imgFillAmount_frost.fillAmount += t;
        }
        if (i == 2)
        {
            imgFillAmount_light.fillAmount += t;
        }
        if (i == 3)
        {
            imgFillAmount_oxygen.fillAmount += t;
        }
    }

    public void setPower(int i,float t)
    {
        if (i == 0)
        {
            imgFillAmount_fire.fillAmount = t;
        }
        if (i == 1)
        {
            imgFillAmount_frost.fillAmount = t;
        }
        if (i == 2)
        {
            imgFillAmount_light.fillAmount = t;
        }
        if (i == 3)
        {
            imgFillAmount_oxygen.fillAmount = t;
        }
    }
    public void CollisionDetection(bool collision)
    {
        if (collision)
        {
            imgFillAmount_oxygen.fillAmount -= 0.001f;
        }
    }
}

        
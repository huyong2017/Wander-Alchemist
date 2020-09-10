using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    // Start is called before the first frame update
    public GameObject Prefabs;
    public GameObject FillAmount_fire;
    public GameObject FillAmount_light;
    public GameObject FillAmount_oxygen;
    public GameObject Img;
    public Image[] Image;
    private Image imgFillAmount_fire;
    private Image imgFillAmount_light;
    private Image imgFillAmount_oxygen;
    float time = 0;
    int count = 0;
    void Start()
    {
        instance = this;
        Prefabs.GetComponent<ParticleSystem>().Stop();
        imgFillAmount_fire = FillAmount_fire.GetComponent<Image>();
        imgFillAmount_light = FillAmount_light.GetComponent<Image>();
        imgFillAmount_oxygen = FillAmount_oxygen.GetComponent<Image>();
    }


    // Update is called once per frame
    public void useFire(bool Fire)//喷射火焰 
    {       
        if (Fire)
        {
            if (imgFillAmount_fire.fillAmount > 0f)
            {
                time += Time.deltaTime;
                Prefabs.GetComponent<ParticleSystem>().Play();
                imgFillAmount_fire.fillAmount = imgFillAmount_fire.fillAmount - time / 5;
            }
            if (imgFillAmount_fire.fillAmount <= 0f)
            {
                Prefabs.GetComponent<ParticleSystem>().Stop();
                imgFillAmount_fire.fillAmount = 0f;
            }
                time = 0;
        }
        if (!Fire)
        {
            Prefabs.GetComponent<ParticleSystem>().Stop();
        }
    }
    public void useLight(bool Light)//发射光
    { 
        if (Light)
        {
            if (imgFillAmount_light.fillAmount > 0f)
            {
                time += Time.deltaTime;
                imgFillAmount_light.fillAmount = imgFillAmount_light.fillAmount - time / 5;
            }
            if(imgFillAmount_light.fillAmount <= 0f)
            {
                imgFillAmount_light.fillAmount = 0f;
            }
            time = 0;
        }
    }
    public void useOxygen(bool Oxygen)//释放氧气
    {       
        if (Oxygen)
        {
            if (imgFillAmount_oxygen.fillAmount > 0f)
            {
                time += Time.deltaTime;
            }
            if (imgFillAmount_oxygen.fillAmount <= 0f)
            {
                imgFillAmount_oxygen.fillAmount = 0f;
            }
            time = 0;
        }
    }
    public void ChangePack(int i)//更换背包
    {
        Img.GetComponent<Image>().sprite = Image[i%3].sprite;
    }
}

        
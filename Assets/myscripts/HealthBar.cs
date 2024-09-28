using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    public Gradient gradient;

    public void SetHealth(int health){
        print("max health : "+health);
        print("max health : "+health);
        print("max health : "+health);
        slider.maxValue = health;
        slider.value=health;
    }

    public void UpdateHealth(int health){
        print("update health : "+health);
        print("update health : "+health);
        print("update health : "+health);
        slider.value=health;
    }

    public float GetHealth(){
        return slider.value;
    }
}

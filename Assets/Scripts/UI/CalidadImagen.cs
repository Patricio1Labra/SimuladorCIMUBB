using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalidadImagen : MonoBehaviour
{
    public Dropdown dropdown;
    public int calidad;
    
    void Start()
    {
        calidad = PlayerPrefs.GetInt("numeroDeCalidad",3);
        dropdown.value = calidad;
        AjustarCalidad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AjustarCalidad()
    {
    	QualitySettings.SetQualityLevel(dropdown.value);
    	PlayerPrefs.SetInt("numeroDeCalidad", dropdown.value);
    	calidad = dropdown.value;
    }
}

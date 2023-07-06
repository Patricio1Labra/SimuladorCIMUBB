using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camaras : MonoBehaviour
{
    public Dropdown camarasDropdown;
    public List <Camera> camaras1 = new List <Camera>();
    void Start()
    {
        LlenarCamaras();
    }

    void Update()
    {
        
    }

    public void LlenarCamaras()
    {
        camarasDropdown.ClearOptions();
        
        camaras1.AddRange(FindObjectsOfType<Camera>(true));
        List<string> opciones = new List<string>();
        int j = 0;
        int activo = 0;
        foreach (var i in camaras1)
        {
            opciones.Add(i.name);
            if(i.isActiveAndEnabled)
            {
                activo = j;
            }
            j++;
        }
        camarasDropdown.AddOptions(opciones);
        camarasDropdown.value = activo;
        camarasDropdown.RefreshShownValue();
    }

    public void CambiarCamara()
    {
        foreach (var i in camaras1)
        {
            i.gameObject.SetActive(false);
            if(string.Compare(i.name,camarasDropdown.options[camarasDropdown.value].text) == 0)
            {
                i.gameObject.SetActive(true);
            }
        }
    }
}

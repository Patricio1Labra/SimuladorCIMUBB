using UnityEngine;
using UnityEngine.UI;

public class ControlDropdown : MonoBehaviour
{
    public MonoBehaviour scriptBrazoUno;
    public MonoBehaviour scriptBrazoDos;
    public MonoBehaviour scriptBrazoTres;
    public MonoBehaviour scriptTomarUno;
    public MonoBehaviour scriptTomarDos;
    public MonoBehaviour scriptTomarTres;
    public GameObject brazoUnoGO;
    public GameObject brazoDosGO;
    public GameObject brazoTresGO;
    public Dropdown dropdown;

    private void Awake()
    {
        // Suscribirnos al evento OnValueChanged del Dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        // Obtenemos el valor actual del Dropdown (0 es el primer ítem, 1 el segundo, etc.)
        if (index == 0)
        {
            // Si se selecciona el primer ítem, desactivamos ScriptDos y ScriptTres, y activamos ScriptUno
            scriptBrazoDos.enabled = false;
            scriptBrazoTres.enabled = false;
            scriptBrazoUno.enabled = true;
            scriptTomarDos.enabled = false;
            scriptTomarTres.enabled = false;
            scriptTomarUno.enabled = true;
            brazoDosGO.SetActive(false);
            brazoTresGO.SetActive(false);
            brazoUnoGO.SetActive(true);
        }
        else if (index == 1)
        {
            // Si se selecciona el segundo ítem, desactivamos ScriptUno y ScriptTres, y activamos ScriptDos
            scriptBrazoUno.enabled = false;
            scriptBrazoTres.enabled = false;
            scriptBrazoDos.enabled = true;
            scriptTomarUno.enabled = false;
            scriptTomarTres.enabled = false;
            scriptTomarDos.enabled = true;
            brazoUnoGO.SetActive(false);
            brazoTresGO.SetActive(false);
            brazoDosGO.SetActive(true);
        }
        else if (index == 2)
        {
            // Si se selecciona el tercer ítem, desactivamos ScriptUno y ScriptDos, y activamos ScriptTres
            scriptBrazoUno.enabled = false;
            scriptBrazoDos.enabled = false;
            scriptBrazoTres.enabled = true;
            scriptTomarUno.enabled = false;
            scriptTomarDos.enabled = false;
            scriptTomarTres.enabled = true;
            brazoUnoGO.SetActive(false);
            brazoDosGO.SetActive(false);
            brazoTresGO.SetActive(true);
        }
    }
}

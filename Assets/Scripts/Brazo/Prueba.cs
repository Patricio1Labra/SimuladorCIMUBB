using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    public float velocidadRotacion = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositiva = KeyCode.A; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativa = KeyCode.D; // Tecla para la rotación en dirección negativa
    public string objetoColision = "Prueba"; // Etiqueta del objeto con el que se produce la colisión

    private bool rotacionHabilitada = true; // Flag para habilitar o deshabilitar la rotación

    private void Update()
    {
        // Verificar si la rotación está habilitada
        if (!rotacionHabilitada)
        {
            return;
        }

        // Obtener la dirección de rotación basada en las teclas presionadas
        float direccionRotacion = 0.0f;

        if (Input.GetKey(teclaRotacionPositiva))
        {
            direccionRotacion = 1.0f;
        }
        else if (Input.GetKey(teclaRotacionNegativa))
        {
            direccionRotacion = -1.0f;
        }

        // Calcular el ángulo de rotación
        float anguloRotacion = direccionRotacion * velocidadRotacion * Time.deltaTime;

        // Realizar la rotación
        transform.Rotate(Vector3.up, anguloRotacion);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión se produjo con el objeto específico
        if (collision.gameObject.CompareTag(objetoColision))
        {
            // Deshabilitar la rotación
            rotacionHabilitada = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verificar si se ha dejado de colisionar con el objeto específico
        if (collision.gameObject.CompareTag(objetoColision))
        {
            // Habilitar la rotación nuevamente
            rotacionHabilitada = true;
        }
    }
}
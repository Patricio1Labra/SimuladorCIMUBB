using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJoints : MonoBehaviour
{
    //Base
    public float velocidadRotacionBase = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaBase = KeyCode.Q; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaBase = KeyCode.A; // Tecla para la rotación en dirección negativa
    public Transform Base; // Transform del objeto que se va a rotar
    private float anguloInicialBase; // Ángulo inicial del objeto

    //Shoulder
    public float velocidadRotacionShoulder = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaShoulder = KeyCode.W; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaShoulder = KeyCode.S; // Tecla para la rotación en dirección negativa
    public Transform Shoulder; // Transform del objeto a rotar
    public Transform puntoFijoShoulder; // Transform del punto fijo alrededor del cual se realizará la rotación
    private float anguloInicialShoulder; // Ángulo inicial del objeto

    //Elbow
    public float velocidadRotacionElbow = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaElbow = KeyCode.E; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaElbow = KeyCode.D; // Tecla para la rotación en dirección negativa
    public Transform Elbow; // Transform del objeto a rotar
    public Transform puntoFijoElbow; // Transform del punto fijo alrededor del cual se realizará la rotación

    //Wrist
    public float velocidadRotacionWrist = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaWrist = KeyCode.R; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaWrist = KeyCode.F; // Tecla para la rotación en dirección negativa
    public Transform Wrist; // Transform del objeto a rotar
    public Transform puntoFijoWrist; // Transform del punto fijo alrededor del cual se realizará la rotación

    //EndEffector
    public float velocidadRotacionEndEffector = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaEndEffector = KeyCode.T; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaEndEffector = KeyCode.G; // Tecla para la rotación en dirección negativa
    public Transform EndEffector; // Transform del objeto a rotar
    public Transform puntoFijoEndEffector; // Transform del punto fijo alrededor del cual se realizará la rotación

    private void Start()
    {
        anguloInicialBase = Base.eulerAngles.y;
        anguloInicialShoulder = Shoulder.localRotation.eulerAngles.z;
    }

    private void Update()
    {
        MovimientoBase();
        MovimientoShoulder();
        MovimientoElbow();
        MovimientoWrist();
        MovimientoEndEffector();
    }

    private void MovimientoBase()
    {
        // Obtener la dirección de rotación basada en las teclas presionadas
        float direccionRotacion = 0.0f;

        if (Input.GetKey(teclaRotacionPositivaBase))
        {
            direccionRotacion = 1.0f;
        }
        else if (Input.GetKey(teclaRotacionNegativaBase))
        {
            direccionRotacion = -1.0f;
        }

        // Verificar si no se están presionando teclas de rotación
        if (direccionRotacion == 0.0f)
        {
            return;
        }

        // Calcular el ángulo de rotación
        float anguloRotacion = direccionRotacion * velocidadRotacionBase * Time.deltaTime;

        // Aplicar la rotación al objeto dentro del rango de límites
        float nuevaRotacion = Base.eulerAngles.y + anguloRotacion;
        nuevaRotacion = Mathf.Clamp(nuevaRotacion, anguloInicialBase - 125.0f, anguloInicialBase + 125.0f);
        Base.eulerAngles = new Vector3(Base.eulerAngles.x, nuevaRotacion, Base.eulerAngles.z);
    }

    private void MovimientoShoulder()
    {
        // Obtener la dirección de rotación basada en las teclas presionadas
        float direccionRotacion = 0.0f;

        if (Input.GetKey(teclaRotacionPositivaShoulder))
        {
            direccionRotacion = 1.0f;
        }
        else if (Input.GetKey(teclaRotacionNegativaShoulder))
        {
            direccionRotacion = -1.0f;
        }

        // Verificar si no se están presionando teclas de rotación
        if (direccionRotacion == 0.0f)
        {
            return;
        }

        // Calcular el ángulo de rotación
        float anguloRotacion = direccionRotacion * velocidadRotacionShoulder * Time.deltaTime;

        // Obtener el ángulo actual de rotación
        float anguloActual = Shoulder.localRotation.eulerAngles.z;

        // Calcular los límites de rotación basados en el ángulo inicial
        float anguloLimitePositivo = anguloInicialShoulder + 10.0f;
        float anguloLimiteNegativo = anguloInicialShoulder - 130.0f;

        // Calcular el ángulo de rotación limitado
        float anguloRotacionLimitado = Mathf.Clamp(anguloActual + anguloRotacion, anguloLimiteNegativo, anguloLimitePositivo) - anguloActual;

        // Realizar la rotación alrededor del punto fijo
        Shoulder.RotateAround(puntoFijoShoulder.position, puntoFijoShoulder.forward, anguloRotacionLimitado);
    }

    private void MovimientoElbow()
    {
        // Obtener la dirección de rotación basada en las teclas presionadas
        float direccionRotacion = 0.0f;

        if (Input.GetKey(teclaRotacionPositivaElbow))
        {
            direccionRotacion = 1.0f;
        }
        else if (Input.GetKey(teclaRotacionNegativaElbow))
        {
            direccionRotacion = -1.0f;
        }

        // Verificar si no se están presionando teclas de rotación
        if (direccionRotacion == 0.0f)
        {
            return;
        }

        // Calcular el ángulo de rotación
        float anguloRotacion = direccionRotacion * velocidadRotacionElbow * Time.deltaTime;

        // Realizar la rotación alrededor del punto fijo
        Elbow.RotateAround(puntoFijoElbow.position, puntoFijoElbow.forward, anguloRotacion);
        
    }

    private void MovimientoWrist()
    {
        // Obtener la dirección de rotación basada en las teclas presionadas
        float direccionRotacion = 0.0f;

        if (Input.GetKey(teclaRotacionPositivaWrist))
        {
            direccionRotacion = 1.0f;
        }
        else if (Input.GetKey(teclaRotacionNegativaWrist))
        {
            direccionRotacion = -1.0f;
        }

        // Verificar si no se están presionando teclas de rotación
        if (direccionRotacion == 0.0f)
        {
            return;
        }

        // Calcular el ángulo de rotación
        float anguloRotacion = direccionRotacion * velocidadRotacionWrist * Time.deltaTime;

        // Realizar la rotación alrededor del punto fijo
        Wrist.RotateAround(puntoFijoWrist.position, puntoFijoWrist.up, anguloRotacion);
    }

    private void MovimientoEndEffector()
    {
        // Obtener la dirección de rotación basada en las teclas presionadas
        float direccionRotacion = 0.0f;

        if (Input.GetKey(teclaRotacionPositivaEndEffector))
        {
            direccionRotacion = 1.0f;
        }
        else if (Input.GetKey(teclaRotacionNegativaEndEffector))
        {
            direccionRotacion = -1.0f;
        }

        // Verificar si no se están presionando teclas de rotación
        if (direccionRotacion == 0.0f)
        {
            return;
        }

        // Calcular el ángulo de rotación
        float anguloRotacion = direccionRotacion * velocidadRotacionEndEffector * Time.deltaTime;

        // Realizar la rotación alrededor del punto fijo
        EndEffector.RotateAround(puntoFijoEndEffector.position, puntoFijoEndEffector.up, anguloRotacion);
    }
}
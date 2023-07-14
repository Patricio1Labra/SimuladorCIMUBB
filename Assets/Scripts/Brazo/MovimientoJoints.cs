using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoJoints : MonoBehaviour
{
    //Base
    public float velocidadRotacionBase = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaBase = KeyCode.Q; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaBase = KeyCode.A; // Tecla para la rotación en dirección negativa
    public Transform Base; // Transform del objeto que se va a rotar
    private float LimitePositivoBase = 125.0f; // Angulo limite del objeto
    private float LimiteNegativoBase = -125.0f; // Angulo limite del objeto
    private float sumaRotacionBase = 0.0f; // Suma de la rotacion del objeto para su uso en limites

    //Shoulder
    public float velocidadRotacionShoulder = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaShoulder = KeyCode.W; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaShoulder = KeyCode.S; // Tecla para la rotación en dirección negativa
    public Transform Shoulder; // Transform del objeto a rotar
    public Transform puntoFijoShoulder; // Transform del punto fijo alrededor del cual se realizará la rotación
    private float LimitePositivoShoulder = 10.0f; // Angulo limite del objeto
    private float LimiteNegativoShoulder = -130.0f; // Angulo limite del objeto
    private float sumaRotacionShoulder = 0.0f; // Suma de la rotacion del objeto para su uso en limites

    //Elbow
    public float velocidadRotacionElbow = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaElbow = KeyCode.E; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaElbow = KeyCode.D; // Tecla para la rotación en dirección negativa
    public Transform Elbow; // Transform del objeto a rotar
    public Transform puntoFijoElbow; // Transform del punto fijo alrededor del cual se realizará la rotación
    public float LimitePositivoElbow = 125.0f; // Angulo limite del objeto
    public float LimiteNegativoElbow = -125.0f; // Angulo limite del objeto
    private float sumaRotacionElbow = 0.0f; // Suma de la rotacion del objeto para su uso en limites

    //Wrist
    public float velocidadRotacionWrist = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaWrist = KeyCode.R; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaWrist = KeyCode.F; // Tecla para la rotación en dirección negativa
    public Transform Wrist; // Transform del objeto a rotar
    public float LimitePositivoWrist= 125.0f; // Angulo limite del objeto
    public float LimiteNegativoWrist = -125.0f; // Angulo limite del objeto
    private float sumaRotacionWrist = 0.0f; // Suma de la rotacion del objeto para su uso en limites

    //EndEffector
    public float velocidadRotacionEndEffector = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaEndEffector = KeyCode.T; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaEndEffector = KeyCode.G; // Tecla para la rotación en dirección negativa
    public Transform EndEffector; // Transform del objeto a rotar
    public float LimitePositivoEndEffector = 125.0f; // Angulo limite del objeto
    public float LimiteNegativoEndEffector = -125.0f; // Angulo limite del objeto
    private float sumaRotacionEndEffector = 0.0f; // Suma de la rotacion del objeto para su uso en limites

    //Botonera
    private string nombreObjeto = "";
    private bool positivoboton = false;
    private bool negativoboton = false;

    private void Start()
    {
        //Arreglando valores para Base
        //StartCoroutine(AdaptarAngulosCoroutine(LimitePositivoBase, valorAjustado =>{LimitePositivoBase = valorAjustado;}));
        //StartCoroutine(AdaptarAngulosCoroutine(LimiteNegativoBase, valorAjustado =>{LimiteNegativoBase = valorAjustado;}));
        //Arreglando valores para Shoulder
        //StartCoroutine(AdaptarAngulosCoroutine(LimitePositivoShoulder, valorAjustado =>{LimitePositivoShoulder = valorAjustado;}));
        //StartCoroutine(AdaptarAngulosCoroutine(LimiteNegativoShoulder, valorAjustado =>{LimiteNegativoShoulder = valorAjustado;}));

        //Arreglando valores para Elbow
        StartCoroutine(AdaptarAngulosCoroutine(LimitePositivoElbow, valorAjustado =>{LimitePositivoElbow = valorAjustado;}));
        StartCoroutine(AdaptarAngulosCoroutine(LimiteNegativoElbow, valorAjustado =>{LimiteNegativoElbow = valorAjustado;}));

        //Arreglando valores para Wrist
        StartCoroutine(AdaptarAngulosCoroutine(LimitePositivoWrist, valorAjustado =>{LimitePositivoWrist = valorAjustado;}));
        StartCoroutine(AdaptarAngulosCoroutine(LimiteNegativoWrist, valorAjustado =>{LimiteNegativoWrist = valorAjustado;}));

        //Arreglando valores para EndEffector
        StartCoroutine(AdaptarAngulosCoroutine(LimitePositivoEndEffector, valorAjustado =>{LimitePositivoEndEffector = valorAjustado;}));
        StartCoroutine(AdaptarAngulosCoroutine(LimiteNegativoEndEffector, valorAjustado =>{LimiteNegativoEndEffector = valorAjustado;}));
    }

    private void Update()
    {
        Movimiento(Base, teclaRotacionPositivaBase, teclaRotacionNegativaBase, velocidadRotacionBase, LimitePositivoBase, LimiteNegativoBase, Base, Base.up, ref sumaRotacionBase);
        Movimiento(Shoulder, teclaRotacionPositivaShoulder, teclaRotacionNegativaShoulder, velocidadRotacionShoulder, LimitePositivoShoulder, LimiteNegativoShoulder, puntoFijoShoulder, puntoFijoShoulder.forward, ref sumaRotacionShoulder);
        Movimiento(Elbow, teclaRotacionPositivaElbow, teclaRotacionNegativaElbow, velocidadRotacionElbow, LimitePositivoElbow, LimiteNegativoElbow, puntoFijoElbow, puntoFijoElbow.forward, ref sumaRotacionElbow);
        Movimiento(Wrist, teclaRotacionPositivaWrist, teclaRotacionNegativaWrist, velocidadRotacionWrist, LimitePositivoWrist, LimiteNegativoWrist, Wrist, Wrist.up, ref sumaRotacionWrist);
        Movimiento(EndEffector, teclaRotacionPositivaEndEffector, teclaRotacionNegativaEndEffector, velocidadRotacionEndEffector, LimitePositivoEndEffector, LimiteNegativoEndEffector, EndEffector, EndEffector.up, ref sumaRotacionEndEffector);
        MovimientoBoton(nombreObjeto);
        
    }
 
    private void Movimiento(Transform objeto, KeyCode TeclaPositiva, KeyCode TeclaNegativa, float VelocidadRotacion, float anguloLimitePositivo, float anguloLimiteNegativo, Transform puntoFijo, Vector3 direccion, ref float Suma)
    {
        
        // Obtener la dirección de rotación basada en las teclas presionadas
        float direccionRotacion = 0.0f;

        if (Input.GetKey(TeclaPositiva))
        {
            direccionRotacion = 1.0f;
        }
        else if (Input.GetKey(TeclaNegativa))
        {
            direccionRotacion = -1.0f;
        }

        // Verificar si no se están presionando teclas de rotación
        if (direccionRotacion == 0.0f)
        {
            return;
        }
        // Calcular el ángulo de rotación
        float anguloRotacion = direccionRotacion * VelocidadRotacion * Time.deltaTime;
        
        // Calcular el ángulo de rotación limitado
        float anguloRotacionLimitado = Mathf.Clamp(Suma, anguloLimiteNegativo, anguloLimitePositivo);
        if(anguloRotacionLimitado == anguloLimiteNegativo && direccionRotacion < 0)
        {
            anguloRotacion = 0.0f;
        }

        if(anguloRotacionLimitado == anguloLimitePositivo && direccionRotacion > 0)
        {
            anguloRotacion = 0.0f;
        }
        // Realizar la rotación alrededor del punto fijo
        objeto.RotateAround(puntoFijo.position, direccion, anguloRotacion);
        Suma += anguloRotacion;
        Suma = Mathf.Clamp(Suma, anguloLimiteNegativo, anguloLimitePositivo);
    }

    private void MovimientoBoton(string nombre)
    {
        
        // Obtener la dirección de rotación basada en las teclas presionadas
        float direccionRotacionboton = 0.0f;

        if (positivoboton)
        {
            direccionRotacionboton = 1.0f;
        }
        if (negativoboton)
        {
            direccionRotacionboton = -1.0f;
        }

        // Verificar si no se están presionando teclas de rotación
        if (direccionRotacionboton == 0.0f)
        {
            return;
        }
        float Sumar = 0;
        Vector3 direccion = Vector3.up;
        float anguloLimitePositivo = 0;
        float anguloLimiteNegativo = 0;
        float VelocidadRotacion = 0;
        Transform puntoFijo = Base;
        Transform objeto = Base;

        if (nombre == "Base")
        {
            Sumar = sumaRotacionBase;
            direccion = Base.up;
            anguloLimitePositivo = LimitePositivoBase;
            anguloLimiteNegativo = LimiteNegativoBase;
            VelocidadRotacion = velocidadRotacionBase;
            puntoFijo = Base;
            objeto = Base;
        }

        if (nombre == "Shoulder")
        {
            Sumar = sumaRotacionShoulder;
            direccion = puntoFijoShoulder.forward;
            anguloLimitePositivo = LimitePositivoShoulder;
            anguloLimiteNegativo = LimiteNegativoShoulder;
            VelocidadRotacion = velocidadRotacionShoulder;
            puntoFijo = puntoFijoShoulder;
            objeto = Shoulder;
        }

        if (nombre == "Elbow")
        {
            Sumar = sumaRotacionElbow;
            direccion = puntoFijoElbow.forward;
            anguloLimitePositivo = LimitePositivoElbow;
            anguloLimiteNegativo = LimiteNegativoElbow;
            VelocidadRotacion = velocidadRotacionElbow;
            puntoFijo = puntoFijoElbow;
            objeto = Elbow;
        }

        if (nombre == "Wrist")
        {
            Sumar = sumaRotacionWrist;
            direccion = Wrist.up;
            anguloLimitePositivo = LimitePositivoWrist;
            anguloLimiteNegativo = LimiteNegativoWrist;
            VelocidadRotacion = velocidadRotacionWrist;
            puntoFijo = Wrist;
            objeto = Wrist;
        }

        if (nombre == "EndEffector")
        {
            Sumar = sumaRotacionEndEffector;
            direccion = EndEffector.up;
            anguloLimitePositivo = LimitePositivoEndEffector;
            anguloLimiteNegativo = LimiteNegativoEndEffector;
            VelocidadRotacion = velocidadRotacionEndEffector;
            puntoFijo = EndEffector;
            objeto = EndEffector;
        }
        
        // Calcular el ángulo de rotación
        float anguloRotacion = direccionRotacionboton * VelocidadRotacion * Time.deltaTime;
        
        // Calcular el ángulo de rotación limitado
        float anguloRotacionLimitado = Mathf.Clamp(Sumar, anguloLimiteNegativo, anguloLimitePositivo);
        if(anguloRotacionLimitado == anguloLimiteNegativo && direccionRotacionboton < 0)
        {
            anguloRotacion = 0.0f;
        }

        if(anguloRotacionLimitado == anguloLimitePositivo && direccionRotacionboton > 0)
        {
            anguloRotacion = 0.0f;
        }
        // Realizar la rotación alrededor del punto fijo
        objeto.RotateAround(puntoFijo.position, direccion, anguloRotacion);
        Sumar += anguloRotacion;
        Sumar = Mathf.Clamp(Sumar, anguloLimiteNegativo, anguloLimitePositivo);

        if (nombre == "Base")
        {
            sumaRotacionBase = Sumar;
        }

        if (nombre == "Shoulder")
        {
            sumaRotacionShoulder = Sumar;
        }

        if (nombre == "Elbow")
        {
            sumaRotacionElbow = Sumar;
        }

        if (nombre == "Wrist")
        {
            sumaRotacionWrist = Sumar;
        }
        if (nombre == "EndEffector")
        {
            sumaRotacionEndEffector = Sumar;
        }
        
    }

    public void PointerUpPositivo(string nombre)
    {
        nombreObjeto = nombre;
        positivoboton = false;
    }

    public void PointerDownPositivo(string nombre)
    {
        nombreObjeto = nombre;
        positivoboton = true;
        
    }

    public void PointerUpNegativo(string nombre)
    {
        nombreObjeto = nombre;
        negativoboton = false;
    }

    public void PointerDownNegativo(string nombre)
    {
        nombreObjeto = nombre;
        negativoboton = true;
    }

    private IEnumerator AdaptarAngulosCoroutine(float angulo, System.Action<float> asignarValor)
    {
        while (true)
        {
            if (angulo > 360.0f)
            {
                angulo -= 360.0f;
            }
            if (angulo < -360.0f)
            {
                angulo += 360.0f;
            }

            // Verificar si el ángulo se ha ajustado correctamente
            if (Mathf.Abs(angulo) <= 360.0f)
            {
                asignarValor(angulo); // Asignar el valor ajustado a la variable correspondiente
                yield break; // Salir del coroutine cuando el ángulo esté ajustado
            }

            yield return null; // Pausar la ejecución hasta el siguiente frame
        }
    }
}
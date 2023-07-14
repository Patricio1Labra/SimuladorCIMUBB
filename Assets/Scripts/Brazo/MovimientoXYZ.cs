using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoXYZ : MonoBehaviour
{
    public Transform EndEffector; // Transform del efector final
    public float velocidadMovimiento = 5f; // Velocidad de movimiento
    public KeyCode teclaPositivaX = KeyCode.Q; // Tecla para moverse en dirección positiva en el eje X
    public KeyCode teclaNegativaX = KeyCode.A; // Tecla para moverse en dirección negativa en el eje X
    public KeyCode teclaPositivaY = KeyCode.W; // Tecla para moverse en dirección positiva en el eje Y
    public KeyCode teclaNegativaY = KeyCode.S; // Tecla para moverse en dirección negativa en el eje Y
    public KeyCode teclaPositivaZ = KeyCode.E; // Tecla para moverse en dirección positiva en el eje Z
    public KeyCode teclaNegativaZ = KeyCode.D; // Tecla para moverse en dirección negativa en el eje Z
    private Vector3 posicionAnterior;   

    public Transform Base;
    public float velocidadRotacionBase = 50.0f;
    public float anguloLimiteNegativoBase = -125.0f;
    public float anguloLimitePositivoBase = 125.0f;
    private float SumaBase = 0.0f;

    public Transform Shoulder;
    public float velocidadRotacionShoulder = 50.0f;
    public float anguloLimiteNegativoShoulder = -125.0f;
    public float anguloLimitePositivoShoulder = 125.0f;
    private float SumaShoulder = 0.0f;

    void Start()
    {
        posicionAnterior = EndEffector.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoverEndEffector();
        MoverBase();
        MoverShoulder();
    }

    private void MoverEndEffector()
    {
        float movimientoHorizontal = 0f;
        float movimientoVertical = 0f;
        float movimientoProfundidad = 0f;

        if (!Input.anyKeyDown)
        {
            //return;
        }
        // Verificar las teclas presionadas y asignar los valores correspondientes
        if (Input.GetKey(teclaPositivaX))
        {
            movimientoHorizontal += 1f;
        }
        if (Input.GetKey(teclaNegativaX))
        {
            movimientoHorizontal -= 1f;
        }
        if (Input.GetKey(teclaPositivaY))
        {
            movimientoVertical += 1f;
        }
        if (Input.GetKey(teclaNegativaY))
        {
            movimientoVertical -= 1f;
        }
        if (Input.GetKey(teclaPositivaZ))
        {
            movimientoProfundidad += 1f;
        }
        if (Input.GetKey(teclaNegativaZ))
        {
            movimientoProfundidad -= 1f;
        }

        // Calcular el desplazamiento del endeffector en los tres ejes
        Vector3 desplazamiento = new Vector3(movimientoHorizontal, movimientoVertical, movimientoProfundidad) * velocidadMovimiento * Time.deltaTime;

        // Aplicar el desplazamiento al endeffector
        EndEffector.position += desplazamiento;

        Vector3 nuevaPosicion = EndEffector.position;

        // Verificar si la posición es la misma que la anterior
        if (nuevaPosicion == posicionAnterior)
        {
            // La posición es la misma, no hacer nada
            return;
        }

        // La posición es diferente, guardar la nueva posición
        posicionAnterior = nuevaPosicion;
    }

    private void MoverBase1()
    {

        // Obtener la posición deseada del efector final en el plano XZ
        Vector3 posicionDeseada = new Vector3(EndEffector.position.x, EndEffector.position.y, EndEffector.position.z);

        // Calcular la dirección desde la base hacia la posición deseada
        Vector3 direccionBaseObjetivo = (posicionDeseada - Base.position).normalized;

        // Calcular el ángulo de rotación
        float anguloRotacion = Mathf.Atan2(direccionBaseObjetivo.z, direccionBaseObjetivo.x) * Mathf.Rad2Deg;

        // Calcular el punto fijo de rotación de la base
        Vector3 puntoFijoBase = Base.position + direccionBaseObjetivo * 0.5f; // Ajusta la distancia según sea necesario
        
        // Calcular el ángulo de rotación limitado
        /* float anguloRotacionLimitado = Mathf.Clamp(Suma, anguloLimiteNegativo, anguloLimitePositivo);
        if(anguloRotacionLimitado == anguloLimiteNegativo)
        {
            anguloRotacion = 0.0f;
        }

        if(anguloRotacionLimitado == anguloLimitePositivo)
        {
            anguloRotacion = 0.0f;
        } */
        // Realizar la rotación alrededor del punto fijo
        Base.RotateAround(Base.position, Base.up, anguloRotacion * Time.deltaTime * velocidadRotacionBase);
        SumaBase += anguloRotacion;
        SumaBase = Mathf.Clamp(SumaBase, anguloLimiteNegativoBase, anguloLimitePositivoBase);
        SumaShoulder = SumaBase;
    }

    private void MoverBase()
    {
        // Obtener la posición actual del objeto de destino
        Vector3 targetPosition = EndEffector.position;

        // Ignorar el componente Y de la posición para mantener el objeto fijo en su plano actual
        targetPosition.y = Base.position.y;

        // Calcular la dirección hacia el objeto de destino
        Vector3 direction = targetPosition - Base.position;

        // Si hay alguna dirección de movimiento
        if (direction.magnitude > 0.01f)
        {
            // Invertir la dirección para que la base mire hacia el objeto de destino
            direction *= -1f;

            // Calcular el ángulo de rotación para alinear la base con la dirección invertida
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Crear una rotación objetivo a partir del ángulo calculado
            Quaternion targetRotation = Quaternion.Euler(-180f, targetAngle -90f, 0f);

            // Interpolar suavemente hacia la rotación objetivo
            Base.rotation = Quaternion.Slerp(Base.rotation, targetRotation, velocidadRotacionBase * Time.deltaTime);
        }
    }

    private void MoverShoulder()
    {
        // Obtener la posición actual del objeto de destino
        Vector3 targetPosition = EndEffector.position;

        // Ignorar el componente Y de la posición para mantener el objeto fijo en su plano actual
        targetPosition.y = Shoulder.position.y;

        // Calcular la dirección hacia el objeto de destino
        Vector3 direction = targetPosition - Shoulder.position;

        // Si hay alguna dirección de movimiento
        if (direction.magnitude > 0.01f)
        {
            // Invertir la dirección para que la base mire hacia el objeto de destino
            direction *= -1f;

            // Calcular el ángulo de rotación para alinear la base con la dirección invertida
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Obtener el punto fijo de rotación para RotateAround
            Vector3 pivotPoint = Shoulder.position + direction.normalized * 1;

            // Calcular el ángulo de rotación actual de la base alrededor del punto fijo
            float currentAngle = Quaternion.LookRotation(-direction).eulerAngles.y;

            // Calcular el ángulo de rotación necesario para alcanzar el objetivo
            float rotationAngle = targetAngle - currentAngle;

            // Aplicar la rotación alrededor del punto fijo
            Shoulder.RotateAround(pivotPoint, Vector3.up, rotationAngle);
        }
    }

}

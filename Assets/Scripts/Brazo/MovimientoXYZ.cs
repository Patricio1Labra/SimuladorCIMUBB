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

    //public Transform base; // Referencia al objeto base (primera articulación)
    public Transform shoulder; // Referencia al objeto shoulder (segunda articulación)
    public Transform elbow; // Referencia al objeto elbow (tercera articulación)
    public Transform wrist; // Referencia al objeto Wrist
    private float baseLenght;
    private float shoulderLenght;
    private float elbowLenght;
    private float wristLenght;

    void Start()
    {
        posicionAnterior = EndEffector.position;
        SetLenghts();
    }

    // Update is called once per frame
    void Update()
    {
        MoverEndEffector();
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

        // Realizar la cinemática inversa para actualizar las articulaciones
        //RealizarCinematicaInversa();
        //SetDirections();
    }

    private void RealizarCinematicaInversa()
    {
        
    }

    private void SetLenghts()
    {
        shoulderLenght = (elbow.position - shoulder.position).magnitude;
        elbowLenght = (EndEffector.position - elbow.position).magnitude;
    }

    private void SetDirections()
    {
        float shoulderToWrist = (wrist.position - shoulder.position).magnitude;

        float shoulderAngle = GetAngle(shoulderLenght, shoulderToWrist, elbowLenght);
        float elbowAngle = GetAngle(elbowLenght, shoulderLenght,shoulderToWrist);

        if (!float.IsNaN(shoulderAngle) && !float.IsNaN(elbowAngle))
        {
            Quaternion shoulderRot = Quaternion.AngleAxis(-shoulderAngle,Vector3.forward);
            shoulder.localRotation = shoulderRot;

            Quaternion elbowRot = Quaternion.AngleAxis(180 - elbowAngle,Vector3.forward);
            elbow.localRotation = elbowRot;
        }
    }

    private float GetAngle (float a, float b, float c)
    {
        return Mathf.Acos((a * a + b * b - c * c) / (2 * a * b)) * Mathf.Rad2Deg;
    }
}

using UnityEngine;

public class CinematicaDirecta : MonoBehaviour
{
    public Transform[] articulaciones; // Array de las articulaciones del brazo robótico
    public Transform efectorFinal; // Transform del efector final

    // Longitudes de los eslabones del brazo robótico Scorbot V Plus
    public float[] longitudes = { 1.0f, 1.0f, 1.0f, 1.0f};

    private void Update()
    {
        // Actualizar la posición del efector final basada en las articulaciones
        ActualizarEfectorFinal();
    }

    private void ActualizarEfectorFinal()
    {
        // Obtener las rotaciones de las articulaciones
        Quaternion[] rotaciones = new Quaternion[articulaciones.Length];
        for (int i = 0; i < articulaciones.Length; i++)
        {
            rotaciones[i] = articulaciones[i].rotation;
        }

        // Calcular la posición y orientación del efector final
        Vector3 posicionEfectorFinal = CalcularPosicionEfectorFinal(rotaciones);
        Quaternion orientacionEfectorFinal = CalcularOrientacionEfectorFinal(rotaciones);

        // Actualizar la posición y orientación del efector final
        efectorFinal.position = posicionEfectorFinal;
        efectorFinal.rotation = orientacionEfectorFinal;
    }

    private Vector3 CalcularPosicionEfectorFinal(Quaternion[] rotaciones)
    {
        Vector3 posicionEfectorFinal = Vector3.zero;

        for (int i = 0; i < rotaciones.Length; i++)
        {
            Vector3 direccion = rotaciones[i] * Vector3.forward;
            posicionEfectorFinal += direccion * longitudes[i];
        }

        return posicionEfectorFinal;
    }

    private Quaternion CalcularOrientacionEfectorFinal(Quaternion[] rotaciones)
    {
        Quaternion orientacionEfectorFinal = Quaternion.identity;

        for (int i = 0; i < rotaciones.Length; i++)
        {
            orientacionEfectorFinal = orientacionEfectorFinal * rotaciones[i];
        }

        return orientacionEfectorFinal;
    }
}

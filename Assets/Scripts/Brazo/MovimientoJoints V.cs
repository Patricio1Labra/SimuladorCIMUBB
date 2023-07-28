using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoJointsV : MonoBehaviour
{
    //Base
    public float velocidadRotacionBase = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaBase = KeyCode.Q; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaBase = KeyCode.A; // Tecla para la rotación en dirección negativa
    public Transform Base; // Transform del objeto que se va a rotar
    private float LimitePositivoBase = 155.0f; // Angulo limite del objeto
    private float LimiteNegativoBase = -155.0f; // Angulo limite del objeto
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
    private float LimitePositivoElbow = 130.0f; // Angulo limite del objeto
    private float LimiteNegativoElbow = -130.0f; // Angulo limite del objeto
    private float sumaRotacionElbow = 0.0f; // Suma de la rotacion del objeto para su uso en limites

    //Wrist
    public float velocidadRotacionWrist = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaWrist = KeyCode.R; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaWrist = KeyCode.F; // Tecla para la rotación en dirección negativa
    public Transform Wrist; // Transform del objeto a rotar
    private float LimitePositivoWrist= 130.0f; // Angulo limite del objeto
    private float LimiteNegativoWrist = -130.0f; // Angulo limite del objeto
    private float sumaRotacionWrist = 0.0f; // Suma de la rotacion del objeto para su uso en limites

    //EndEffector
    public float velocidadRotacionEndEffector = 50.0f; // Velocidad de rotación
    public KeyCode teclaRotacionPositivaEndEffector = KeyCode.T; // Tecla para la rotación en dirección positiva
    public KeyCode teclaRotacionNegativaEndEffector = KeyCode.G; // Tecla para la rotación en dirección negativa
    public Transform EndEffector; // Transform del objeto a rotar
    private float LimitePositivoEndEffector = 570.0f; // Angulo limite del objeto
    private float LimiteNegativoEndEffector = -570.0f; // Angulo limite del objeto
    private float sumaRotacionEndEffector = 0.0f; // Suma de la rotacion del objeto para su uso en limites

    //Botonera
    private string nombreObjeto = "";
    private bool positivoboton = false;
    private bool negativoboton = false;

    private bool isControlPressed = false;
    private bool isAltPressed = false;

    private bool EmpezarGuardar = false;
    private bool TerminarGuardar = false;
    private bool EmpezarMover = false;
    private bool TerminarMover = false;
    private int NumeroUsar = -1;

    private Dictionary<int, float> GuardarBase = new Dictionary<int, float>();
    private Dictionary<int, float> GuardarShoulder = new Dictionary<int, float>();
    private Dictionary<int, float> GuardarElbow = new Dictionary<int, float>();
    private Dictionary<int, float> GuardarWrist = new Dictionary<int, float>();
    private Dictionary<int, float> GuardarEndEffector = new Dictionary<int, float>();

    private void Start()
    {
        GuardarBase.Add(0, 0f);
        GuardarShoulder.Add(0,0f);
        GuardarElbow.Add(0,0f);
        GuardarWrist.Add(0,0f);
        GuardarEndEffector.Add(0,0f);
    }

    private void Update()
    {
        Movimiento(Base, teclaRotacionPositivaBase, teclaRotacionNegativaBase, velocidadRotacionBase, LimitePositivoBase, LimiteNegativoBase, Base, -Base.up, ref sumaRotacionBase);
        Movimiento(Shoulder, teclaRotacionPositivaShoulder, teclaRotacionNegativaShoulder, velocidadRotacionShoulder, LimitePositivoShoulder, LimiteNegativoShoulder, puntoFijoShoulder, puntoFijoShoulder.forward, ref sumaRotacionShoulder);
        Movimiento(Elbow, teclaRotacionPositivaElbow, teclaRotacionNegativaElbow, velocidadRotacionElbow, LimitePositivoElbow, LimiteNegativoElbow, puntoFijoElbow, puntoFijoElbow.forward, ref sumaRotacionElbow);
        Movimiento(Wrist, teclaRotacionPositivaWrist, teclaRotacionNegativaWrist, velocidadRotacionWrist, LimitePositivoWrist, LimiteNegativoWrist, Wrist, Wrist.up, ref sumaRotacionWrist);
        Movimiento(EndEffector, teclaRotacionPositivaEndEffector, teclaRotacionNegativaEndEffector, velocidadRotacionEndEffector, LimitePositivoEndEffector, LimiteNegativoEndEffector, EndEffector, EndEffector.up, ref sumaRotacionEndEffector);
        MovimientoBoton(nombreObjeto);
        Guardar();
        Usar();
        GuardarBoton();
        UsarBoton();
        
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

    private void Guardar()
    {
        // Verificar si se presionó la tecla de control (Ctrl)
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            isControlPressed = true;
        }

        // Verificar si está habilitado el registro de teclas
        if (isControlPressed)
        {
            // Verificar las teclas numéricas del 1 al 9
            for (int i = 1; i <= 9; i++)
            {
                if (Input.GetKey(i.ToString()))
                {
                    SaveInformation(i);
                    isControlPressed = false;
                }
            }
        }
    }

    private void GuardarBoton()
    {
        // Verificar si se presionó el boton
        if (EmpezarGuardar)
        {
            // Verificar si se habilita el guardado
            if (TerminarGuardar)
            {
                SaveInformation(NumeroUsar);
                TerminarGuardar = false;
                EmpezarGuardar = false;
                NumeroUsar = -1;
            }
        }     
    }

    private void Usar()
    {
        // Verificar si se presionó la tecla de control (Ctrl)
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            isAltPressed = true;
        }

        // Verificar si está habilitado el registro de teclas
        if (isAltPressed)
        {
            // Verificar las teclas numéricas del 0 al 9
            for (int i = 0; i <= 9; i++)
            {
                if (Input.GetKey(i.ToString()))
                {
                    GetInformation(i);
                    isAltPressed = false;
                }
            }
        }
    }

    private void UsarBoton()
    {
        // Verificar si se presionó el boton
        if (EmpezarMover)
        {
            // Verificar si se habilita el guardado
            if (TerminarMover)
            {
                GetInformation(NumeroUsar);
                TerminarMover = false;
                EmpezarMover = false;
                NumeroUsar = -1;
            }
        }
    }

    private void SaveInformation(int i)
    {
        if (GuardarBase.ContainsKey(i))
        {
            GuardarBase[i] = sumaRotacionBase;
        }
        else
        {
            GuardarBase.Add(i,sumaRotacionBase);
        }

        if (GuardarShoulder.ContainsKey(i))
        {
            GuardarShoulder[i] = sumaRotacionShoulder;
        }
        else
        {
            GuardarShoulder.Add(i, sumaRotacionShoulder);
        }

        if (GuardarElbow.ContainsKey(i))
        {
            GuardarElbow[i] = sumaRotacionElbow;
        }
        else
        {
            GuardarElbow.Add(i, sumaRotacionElbow);
        }

        if (GuardarWrist.ContainsKey(i))
        {
            GuardarWrist[i] = sumaRotacionWrist;
        }
        else
        {
            GuardarWrist.Add(i, sumaRotacionWrist);
        }

        if (GuardarEndEffector.ContainsKey(i))
        {
            GuardarEndEffector[i] = sumaRotacionEndEffector;
        }
        else
        {
            GuardarEndEffector.Add(i, sumaRotacionEndEffector);
        }
    }

    private void GetInformation(int i)
    {
        if (GuardarBase.ContainsKey(i))
        {
            float value = GuardarBase[i];
            Debug.Log("Base "+ i + " = " + value);
            //MoverGuardado(value,sumaRotacionBase,Base,Base,LimiteNegativoBase,LimitePositivoBase,velocidadRotacionBase,-Base.up);
        }
        else
        {
            Debug.Log("no existe");
        }

        if (GuardarShoulder.ContainsKey(i))
        {
            float value = GuardarShoulder[i];
            Debug.Log("Shoulder "+ i + " = " + value);
        }
        else
        {
            Debug.Log("no existe");
        }

        if (GuardarElbow.ContainsKey(i))
        {
            float value = GuardarElbow[i];
            Debug.Log("Elbow "+ i + " = " + value);
        }
        else
        {
            Debug.Log("no existe");
        }

        if (GuardarWrist.ContainsKey(i))
        {
            float value = GuardarWrist[i];
            Debug.Log("Wrist "+ i + " = " + value);
        }
        else
        {
            Debug.Log("no existe");
        }

        if (GuardarEndEffector.ContainsKey(i))
        {
            float value = GuardarEndEffector[i];
            Debug.Log("EndEffector "+ i + " = " + value);
        }
        else
        {
            Debug.Log("no existe");
        }
    }

    public void EmpezarAGuardar()
    {
        EmpezarGuardar = true;
    }

    public void NumeroAUsar(int i)
    {
        if(EmpezarGuardar || EmpezarMover)
        {
            NumeroUsar = i;
        }
    }

    public void TerminarAGuardar()
    {
        if(0 < NumeroUsar && NumeroUsar < 10)
        {
            if(EmpezarGuardar)
            {
                TerminarGuardar = true;
            }
        }
    }

    public void EmpezarAMover()
    {
        EmpezarMover = true;
    }

    public void TerminarAMover()
    {
        if(-1 < NumeroUsar && NumeroUsar < 10)
        {
            if(EmpezarMover)
            {
                TerminarMover = true;
            }
            
        }
    }

    private void MoverGuardado(float valor, float Suma, Transform puntoFijo, Transform objeto, float anguloLimiteNegativo, float anguloLimitePositivo, float VelocidadRotacion, Vector3 direccion)
    {
        
        float direccionRotacion = 0.0f;

        if (Suma < valor)
        {
            direccionRotacion = 1.0f;
        }
        else if (Suma > valor)
        {
            direccionRotacion = -1.0f;
        }

        if (direccionRotacion == 0.0f)
        {
            return;
        }
        // Calcular el ángulo de rotación
        
        // Realizar la rotación alrededor del punto fijo
        while(Suma != valor)
        {
            float anguloRotacion = direccionRotacion * VelocidadRotacion * Time.deltaTime;
            objeto.RotateAround(puntoFijo.position, direccion, anguloRotacion);
            Suma += anguloRotacion;
            if(direccionRotacion == 1.0f)
            {
                Suma = Mathf.Clamp(Suma, anguloLimiteNegativo, valor);
            }
            else
            {
                Suma = Mathf.Clamp(Suma, valor, anguloLimitePositivo);
            }
            
        }
        
    }
}
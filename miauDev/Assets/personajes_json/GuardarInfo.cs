using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class GuardarInfo : MonoBehaviour
{
    private string rutaArchivo;
    public InputField inputNombre; 
    public Text textoNombreActual;
    private Protagonista personajeActual;
    private void Start()
    {
        rutaArchivo = Path.Combine(Application.persistentDataPath, "protagonista.json");

        // Crear un personaje de ejemplo y guardarlo
        //Protagonista personaje = CrearPersonajeEjemplo();
        //GuardarPersonaje(personaje);

        // Cargar los datos del archivo y mostrarlos
        //Protagonista cargado = CargarPersonaje();
        //MostrarDatos(cargado);

        personajeActual = CargarPersonaje();

        if (!string.IsNullOrEmpty(personajeActual.nombre))
        {
            textoNombreActual.text = "Nombre actual: " + personajeActual.nombre;
            inputNombre.text = personajeActual.nombre;
        }
    }

    private Protagonista CrearPersonajeEjemplo()
    {
        Protagonista pj = new Protagonista();
        pj.nombre = "Josue";
        pj.cordura = 5;
        pj.salud = 95.5f;
        pj.percepcion = 230.4f;
        pj.posicion = new Vector3(10.5f, 1.2f, -3.8f);

        pj.inventario.Add(new Item { nombre = "Espada de Fuego", cantidad = 1 });
        pj.inventario.Add(new Item { nombre = "Poción de Vida", cantidad = 3 });
        pj.inventario.Add(new Item { nombre = "Llave Dorada", cantidad = 1 });

        return pj;
    }

    public void GuardarPersonaje(Protagonista datos)
    {
        
        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(rutaArchivo, json);
        Debug.Log("Datos guardados en: " + rutaArchivo);
    }

    public Protagonista CargarPersonaje()
    {
        
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            Protagonista datos = JsonUtility.FromJson<Protagonista>(json);
            Debug.Log("Datos cargados desde: " + rutaArchivo);
            return datos;
        }
        else
        {
            Debug.Log("No se encontró el archivo, creando nuevo personaje...");
            return new Protagonista();
        }
    }

    private void MostrarDatos(Protagonista p)
    {
        Debug.Log($"Nombre: {p.nombre}");
        Debug.Log($"Nivel: {p.cordura}");
        Debug.Log($"Salud: {p.salud}");
        Debug.Log($"Experiencia: {p.percepcion}");
        Debug.Log($"Posición: {p.posicion}");
        foreach (var item in p.inventario)
        {
            Debug.Log($"Item: {item.nombre} x{item.cantidad}");
        }
    }
    public void GuardarNombreDesdeUI()
    {
        personajeActual.nombre = inputNombre.text;
        GuardarPersonaje(personajeActual);
        textoNombreActual.text = "Nombre actual: " + personajeActual.nombre;
        Debug.Log("Nombre guardado: " + personajeActual.nombre);
    }

   

    
}

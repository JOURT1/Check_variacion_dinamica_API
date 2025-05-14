using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class RespuestaAPI
{
    public int id;
    public int preguntaId;
    public string textoRespuesta;
    public bool esCorrecta;
}

public class PlayerColorController : MonoBehaviour
{
    public string apiURL = "https://backendroblox.azurewebsites.net/api/respuesta";

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ObtenerColorDeAPI());
    }

    IEnumerator ObtenerColorDeAPI()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiURL))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al llamar a la API: " + request.error);
            }
            else
            {
                // El JSON que devuelve es un array, entonces primero lo obtenemos como texto
                string json = request.downloadHandler.text;

                // Quitar los corchetes [] para deserializar solo el objeto
                json = json.TrimStart('[').TrimEnd(']');

                RespuestaAPI respuesta = JsonUtility.FromJson<RespuestaAPI>(json);

                Debug.Log("Color recibido: " + respuesta.textoRespuesta);

                CambiarColor(respuesta.textoRespuesta);
            }
        }
    }

    void CambiarColor(string colorNombre)
    {
        Color nuevoColor;

        // Intenta convertir el nombre a Color Unity
        if (ColorUtility.TryParseHtmlString(GetColorHTML(colorNombre), out nuevoColor))
        {
            spriteRenderer.color = nuevoColor;
        }
        else
        {
            Debug.LogWarning("Color no reconocido: " + colorNombre + ". Se usará color blanco por defecto.");
            spriteRenderer.color = Color.white;
        }
    }

    // Aquí mapeamos nombres simples a códigos hex HTML para colores básicos
    string GetColorHTML(string nombre)
    {
        switch(nombre.ToLower())
        {
            case "red": return "#FF0000";
            case "green": return "#00FF00";
            case "blue": return "#0000FF";
            case "yellow": return "#FFFF00";
            case "black": return "#000000";
            case "white": return "#FFFFFF";
            case "cyan": return "#00FFFF";
            case "magenta": return "#FF00FF";
            case "gray": return "#808080";
            // Añade más si quieres...
            default: return "#FFFFFF"; // blanco por defecto
        }
    }
}

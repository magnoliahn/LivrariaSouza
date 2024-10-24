using System.Text.Json;

public static class SessionExtensions
{
    // Salva um objeto na sessão como JSON
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        var jsonData = JsonSerializer.Serialize(value); // Converte para JSON
        session.SetString(key, jsonData); // Armazena como string na sessão
    }

    // Recupera um objeto da sessão a partir de JSON
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var jsonData = session.GetString(key);
        return jsonData == null ? default : JsonSerializer.Deserialize<T>(jsonData);
    }
}

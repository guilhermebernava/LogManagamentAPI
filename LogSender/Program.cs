using WebSocketSharp;

using (var ws = new WebSocket("ws://localhost:7276/logHub"))
{
    ws.OnOpen += (sender, e) =>
    {
        Console.WriteLine("Conectado ao WebSocket.");

        // Enviar um log
        var log = new
        {
            Id = Guid.NewGuid().ToString(),
            Level = "Info",
            Message = "Este é um teste de log enviado via WebSocket.",
            Timestamp = DateTime.UtcNow.ToString("o")
        };

        var logJson = System.Text.Json.JsonSerializer.Serialize(log);
        ws.Send(logJson);

        Console.WriteLine("Log enviado: " + logJson);
    };

    ws.OnMessage += (sender, e) =>
    {
        Console.WriteLine("Mensagem recebida: " + e.Data);
    };

    ws.Connect();

    Console.WriteLine("Pressione qualquer tecla para sair...");
    Console.ReadKey();
}
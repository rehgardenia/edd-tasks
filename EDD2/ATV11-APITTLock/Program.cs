
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DotNetEnv;

// 1. Carregar variáveis do arquivo .env
Env.Load();

string baseUrl = ObterVariavel("TTLOCK_BASE_URL").TrimEnd('/');
string clientId = ObterVariavel("TTLOCK_CLIENT_ID");
string clientSecret = ObterVariavel("TTLOCK_CLIENT_SECRET");
string username = ObterVariavel("TTLOCK_USERNAME");
string password = ObterVariavel("TTLOCK_PASSWORD");

// 2. Converter senha para MD5 minúsculo
string passwordMd5 = Convert.ToHexString(
    MD5.HashData(Encoding.UTF8.GetBytes(password))
).ToLowerInvariant();

// 3. Criar cliente HTTP
using HttpClient http = new HttpClient
{
    Timeout = TimeSpan.FromSeconds(30)
};

// 4. Preparar os parâmetros da autenticação
var parametros = new Dictionary<string, string>
{
    ["client_id"] = clientId,
    ["client_secret"] = clientSecret,
    ["username"] = username,
    ["password"] = passwordMd5
};

try
{
    // 5. Enviar requisição OAuth
    using var conteudo = new FormUrlEncodedContent(parametros);

    using HttpResponseMessage resposta = await http.PostAsync(
        $"{baseUrl}/oauth2/token",
        conteudo
    );

    string json = await resposta.Content.ReadAsStringAsync();

    // 6. Verificar resposta HTTP
    if (!resposta.IsSuccessStatusCode)
    {
        Console.WriteLine(
            $"Erro HTTP: {(int)resposta.StatusCode}"
        );
        Console.WriteLine("Falha na autenticação.");
        return;
    }

    // 7. Interpretar resposta JSON
    using JsonDocument documento = JsonDocument.Parse(json);

    JsonElement dados = documento.RootElement;

    if (dados.TryGetProperty("access_token", out JsonElement token)
        && !string.IsNullOrWhiteSpace(token.GetString()))
    {
        string accessToken = token.GetString()!;

        Console.WriteLine("Autenticação realizada com sucesso!");
        Console.WriteLine("Access token recebido.");

        if (dados.TryGetProperty("uid", out var uid))
        {
            Console.WriteLine($"UID: {uid}");
        }

        if (dados.TryGetProperty("expires_in", out var validade))
        {
            Console.WriteLine($"Validade: {validade} segundos");
        }

        // Guardar accessToken para a próxima etapa:
        // listagem de fechaduras.
    }
    else
    {
        string erro = dados.TryGetProperty("errmsg", out var mensagem)
            ? mensagem.ToString()
            : "Resposta sem access_token.";

        Console.WriteLine($"Erro na autenticação: {erro}");
    }
}
catch (HttpRequestException)
{
    Console.WriteLine(
        "Erro de conexão HTTP. Verifique a URL, a rede e o servidor."
    );
}
catch (TaskCanceledException)
{
    Console.WriteLine(
        "Tempo limite da requisição excedido."
    );
}
catch (JsonException)
{
    Console.WriteLine(
        "A API retornou uma resposta JSON inválida."
    );
}

// Método auxiliar para validar variáveis de ambiente
static string ObterVariavel(string nome)
{
    string? valor = Environment.GetEnvironmentVariable(nome);

    if (string.IsNullOrWhiteSpace(valor))
    {
        throw new InvalidOperationException(
            $"Variável de ambiente ausente: {nome}"
        );
    }

    return valor;
}

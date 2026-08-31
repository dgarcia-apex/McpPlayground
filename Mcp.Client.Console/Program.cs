using Mcp.Client.Console.Services;
using Shared.Contracts;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5177")
};

var hostClient = new HostClient(httpClient);

try
{
    Console.WriteLine("Loading capabilities...");
    Console.WriteLine();

    var capabilities =
        await hostClient.GetCapabilitiesAsync();

    if (capabilities.Count == 0)
    {
        Console.WriteLine(
            "No capabilities available.");

        return;
    }

    DisplayCapabilities(capabilities);

    var selectedTool =
        GetToolSelection(capabilities);

    var parameters =
        CollectParameters(selectedTool);

    var request =
        new ToolCallRequest(
            selectedTool,
            parameters);

    Console.WriteLine();
    Console.WriteLine(
        $"Executing '{selectedTool}'...");
    Console.WriteLine();

    var response =
        await hostClient.ExecuteAsync(
            request);

    Console.WriteLine();
    Console.WriteLine("Response:");
    Console.WriteLine();

    Console.WriteLine(
        response?.Result ?? "No response");
}
catch (Exception ex)
{
    Console.WriteLine(
        $"Error: {ex.Message}");
}

static void DisplayCapabilities(
    List<string> capabilities)
{
    Console.WriteLine(
        "Available tools:");

    for (int i = 0; i < capabilities.Count; i++)
    {
        Console.WriteLine(
            $"{i + 1}. {capabilities[i]}");
    }

    Console.WriteLine();
}

static string GetToolSelection(
    List<string> capabilities)
{
    while (true)
    {
        Console.Write(
            "Choose a tool: ");

        var input =
            Console.ReadLine();

        if (!int.TryParse(
            input,
            out var selection))
        {
            Console.WriteLine(
                "Invalid selection.");
            continue;
        }

        if (selection < 1 ||
            selection > capabilities.Count)
        {
            Console.WriteLine(
                "Selection out of range.");
            continue;
        }

        return capabilities[
            selection - 1];
    }
}

static Dictionary<string, string>
    CollectParameters(
        string toolName)
{
    var parameters =
        new Dictionary<string, string>();

    switch (toolName)
    {
        case "tell_joke":

            Console.Write(
                "Joke type: ");

            parameters["type"] =
                Console.ReadLine()
                ?? "short";

            break;

        case "tell_story":

            Console.Write(
                "Story topic: ");

            parameters["topic"] =
                Console.ReadLine()
                ?? "coding";

            break;

        case "bible_study":

            Console.Write(
                "Bible study topic: ");

            parameters["topic"] =
                Console.ReadLine()
                ?? "faith";

            break;
    }

    return parameters;
}
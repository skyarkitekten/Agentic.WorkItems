using Azure;
using Azure.Identity;
using Azure.AI.Inference;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
var endpointUrl = new Uri(config["endpointUrl"] ?? "");

var credential = new DefaultAzureCredential();

AzureAIInferenceClientOptions clientOptions = new AzureAIInferenceClientOptions();
BearerTokenAuthenticationPolicy tokenPolicy = new BearerTokenAuthenticationPolicy(
    credential,
    ["https://cognitiveservices.azure.com/.default"]
);
clientOptions.AddPolicy(tokenPolicy, HttpPipelinePosition.PerRetry);

var client = new ChatCompletionsClient(
    endpointUrl,
    credential,
    clientOptions
);

using System.Text;
using System.Text.Json;
using ServiceProvider.Application.Interfaces;
using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.Application.Services;

public class TokenIssuanceClient : ITokenIssuanceClient
{
    private readonly HttpClient _httpClient;

    public TokenIssuanceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Token>?> GetPendingTokens()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/tokens/pending");

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var tokens = await JsonSerializer.DeserializeAsync<IEnumerable<Token>>(responseStream);

            return tokens;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserialization error: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> UpdateToken(int tokenId, TokenStatus status)
    {
        try
        {
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response =
                await _httpClient.PutAsync($"api/tokens/update-token?id={tokenId}&tokenStatus={(int) status}", content);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while updating the token: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return false;
        }
    }
}
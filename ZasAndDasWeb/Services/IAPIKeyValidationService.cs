namespace ZasAndDasWeb.Services
{
    public interface IAPIKeyValidationService
    {
        bool IsValidAPIKey(string? key);
    }
}
using FicheUEAuthApi.Models;

namespace FicheUEAuthApi.ModelsDTO;

public class UserDTO
{
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

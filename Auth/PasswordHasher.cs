using System.Security.Cryptography;
using System.Text;

namespace ClinicManagmentAPIs.Auth;

public class PasswordHasher : IPasswordHasher
{
    private const string Prefix = "sha256:";

    public string Hash(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Prefix + Convert.ToBase64String(bytes);
    }

    public bool Verify(string password, string storedHash)
    {
        if (string.IsNullOrEmpty(storedHash))
            return false;

        if (storedHash.StartsWith(Prefix, StringComparison.Ordinal))
            return storedHash == Hash(password);

        // Legacy rows may still store plain text in password_hash.
        return password == storedHash;
    }
}

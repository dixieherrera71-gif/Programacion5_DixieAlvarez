using System.Security.Cryptography;

namespace BellezaMas.Api.Servicios;

public class ContrasenaServicio
{
    private const int Iteraciones = 100000;
    private const int TamanoSalt = 16;
    private const int TamanoHash = 32;

    public string CrearHash(string contrasena)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(TamanoSalt);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            contrasena,
            salt,
            Iteraciones,
            HashAlgorithmName.SHA256,
            TamanoHash);

        return $"PBKDF2${Iteraciones}$" +
               $"{Convert.ToBase64String(salt)}$" +
               $"{Convert.ToBase64String(hash)}";
    }

    public bool Verificar(string contrasena, string hashGuardado)
    {
        try
        {
            string[] partes = hashGuardado.Split('$');

            if (partes.Length != 4 || partes[0] != "PBKDF2")
                return false;

            int iteraciones = int.Parse(partes[1]);
            byte[] salt = Convert.FromBase64String(partes[2]);
            byte[] hashEsperado = Convert.FromBase64String(partes[3]);

            byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                contrasena,
                salt,
                iteraciones,
                HashAlgorithmName.SHA256,
                hashEsperado.Length);

            return CryptographicOperations.FixedTimeEquals(
                hashCalculado,
                hashEsperado);
        }
        catch
        {
            return false;
        }
    }
}
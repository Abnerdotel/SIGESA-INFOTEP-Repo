using System.Security.Cryptography;
using System.Text;

namespace SigesaData.Utilidades
{
    public class Encriptador
    {
            public static string HashearClave(string clave)
            {
                return BCrypt.Net.BCrypt.HashPassword(clave);
            }

            public static bool VerificarClave(string claveTextoPlano, string hashGuardado)
            {
                return BCrypt.Net.BCrypt.Verify(claveTextoPlano, hashGuardado);
            }

    }
}

namespace impresion3d.Core.Servicios
{
    public static class ServicioPassword
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerificarPassword(
            string password,
            string? hash)
        {
            if (string.IsNullOrEmpty(hash))
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
} 
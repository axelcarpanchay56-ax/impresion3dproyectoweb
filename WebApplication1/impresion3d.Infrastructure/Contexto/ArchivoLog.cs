using System.IO;

namespace impresion3d.Infrastructure.Contexto
{
    public static class ArchivoLog
    {
        private static readonly string LogPath = "errores.txt";

        public static void RegistrarErrores(Exception ex)
        {
            string mensaje = $"{DateTime.Now}: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
            File.AppendAllText(LogPath, mensaje);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Constants
{
    public static class Priorities
    {
        public const string LOW = "baja";
        public const string MEDIUM = "media";
        public const string HIGH = "alta";
        public const string CRITICAL = "critica";
    }

    public static class States
    {
        public const string NEW = "Nuevo";
        public const string IN_PROGRESS = "En Progreso";
        public const string RESOLVED = "Resuelto";
        public const string CLOSED = "Cerrado";
    }
}

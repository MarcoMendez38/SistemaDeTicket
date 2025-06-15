using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevaAplicacion.Models
{
    public class PriorityFlyweight
    {
        private readonly string _name;
        private readonly int _level;
        private readonly string _color;

        private PriorityFlyweight(string name, int level, string color)
        {
            _name = name;
            _level = level;
            _color = color;
        }

        public string Name => _name;
        public int Level => _level;
        public string Color => _color;

        private static readonly Dictionary<string, PriorityFlyweight> _priorities =
            new Dictionary<string, PriorityFlyweight>();

        public static PriorityFlyweight GetPriority(string name)
        {
            if (!_priorities.ContainsKey(name))
            {
                switch (name.ToLower())
                {
                    case "baja":
                        _priorities[name] = new PriorityFlyweight("Baja", 1, "Green");
                        break;
                    case "media":
                        _priorities[name] = new PriorityFlyweight("Media", 2, "Orange");
                        break;
                    case "alta":
                        _priorities[name] = new PriorityFlyweight("Alta", 3, "Red");
                        break;
                    case "critica":
                        _priorities[name] = new PriorityFlyweight("Crítica", 4, "DarkRed");
                        break;
                    default:
                        _priorities[name] = new PriorityFlyweight("Media", 2, "Orange");
                        break;
                }
            }
            return _priorities[name];
        }
    }
}
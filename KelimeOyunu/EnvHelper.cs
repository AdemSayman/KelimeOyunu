using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelimeOyunu
{
    public static class EnvHelper
    {
        private static Dictionary<string, string> _envVariables = null;

        public static string GetEnv(string key)
        {
            if (_envVariables == null)
                LoadEnv();

            return _envVariables.ContainsKey(key) ? _envVariables[key] : null;
        }

        private static void LoadEnv()
        {
            _envVariables = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(".env");

            foreach (var line in lines)
            {
                if (line.StartsWith("#") || !line.Contains("="))
                    continue;

                var parts = line.Split('=', 2);
                var envKey = parts[0].Trim();
                var envValue = parts[1].Trim();

                _envVariables[envKey] = envValue;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Model;

namespace Sat.Recruitment.Api.CrossCore
{
    public static class Common
    {
        public static async Task<IEnumerable<User>> ReadUsersFromFile(string file)
        {
            var response = new List<User>();
            var path = Path.Combine(Directory.GetCurrentDirectory(), file);
            using var reader = new StreamReader(new FileStream(path, FileMode.Open));
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                if (line == null) continue;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                response.Add(user);
            }
            return response;
        }
        public static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", string.Empty) : aux[0].Replace(".", string.Empty).Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
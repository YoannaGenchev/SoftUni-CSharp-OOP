using System.Reflection;

namespace AuthorProblem;

public class Tracker
{
    public void PrintMethodsByAuthor()
    {
        var type = typeof(StartUp);
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

        foreach (var method in methods)
        {
            if (method.CustomAttributes.Any(n => n.AttributeType == typeof(AuthorAttribute)))
            {
                var authorAttributes = method.GetCustomAttributes<AuthorAttribute>();
                foreach (var authorAttribute in authorAttributes)
                    Console.WriteLine($"{method.Name} is written by {authorAttribute.Name}");
            }
        }
    }
}
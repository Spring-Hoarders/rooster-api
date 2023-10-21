using System.Reflection;

namespace Rooster.Infrastructure;

public static class AboutMe
{
    public static Assembly Assembly => typeof(AboutMe).Assembly;
}
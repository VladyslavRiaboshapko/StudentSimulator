namespace StudentSimulator.Exceptions;

public class NavigationException : Exception
{
    public NavigationTarget Target { get; }
    public NavigationException(NavigationTarget target) => Target = target;
}
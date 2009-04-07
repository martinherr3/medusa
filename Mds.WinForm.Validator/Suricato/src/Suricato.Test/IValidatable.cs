namespace Suricato.Test
{
    public interface IValidatable
    {
        bool IsValid();
        string[] ValidationErrorMessages { get;}
    }
}
namespace Suricato.Business
{
    public interface IValidatable
    {
        bool IsValid();
        string[] ValidationErrorMessages { get;}
    }
}
namespace Suricato.Business
{
    public interface IEntity<TId> 
    {
        TId Id { get; }
        bool IsTransient { get; }
    }
}
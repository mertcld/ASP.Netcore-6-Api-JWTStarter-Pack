namespace JwtStarterApi.EntityFrameworkCore.Models
{
    public interface IHasOwner
    {
        string OwnerId { get; }
    }
}

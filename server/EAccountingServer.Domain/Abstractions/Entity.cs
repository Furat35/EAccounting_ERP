namespace EAccountingServer.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}

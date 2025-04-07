namespace ProductManagement.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get;  protected set; }
        public DateTime CreatedAt { get;  private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedAt { get; private set; }
        public bool IsActive { get; private set; } = true;

        public void SetActive() => IsActive = true;
        public void SetInactive() => IsActive = false;
        public void UpdateLastUpdatedAt() => LastUpdatedAt = DateTime.UtcNow;
        
    }
}

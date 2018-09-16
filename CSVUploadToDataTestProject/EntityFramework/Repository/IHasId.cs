namespace CSVUploadToDataProject.EntityFramework.Repository
{
    public interface IHasId<TId>
    {
        TId Id { get; set; }
    }
}

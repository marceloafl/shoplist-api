namespace ShoplistAPI.Repository
{
    public interface IUnitOfWork
    {
        IShoplistRepository ShoplistRepository { get; }
        IProductRepository ProductRepository { get; }
        Task Commit ();
    }
}

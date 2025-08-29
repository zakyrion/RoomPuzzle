using Inventory.UIs;

namespace Inventory.Providers
{
    public interface IInventoryProvider
    {
        IInventoryUI GetInventoryUI();
        void DisposeInventoryUI();
    }
}
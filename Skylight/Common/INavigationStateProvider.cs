
namespace Skylight.Common
{
    public interface INavigationStateProvider
    {
        void LoadNavigationState(WrappedLoadStateEventArgs args);

        void SaveNavigationState(WrappedSaveStateEventArgs args);
    }
}

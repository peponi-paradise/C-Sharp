using Prism.Commands;
using Prism.Regions;
using System.Threading.Tasks;

namespace ViewModel.Components;

public class NavigatorViewModel
{
    private readonly IRegionManager? _RegionManager;

    public DelegateCommand<string> NavigatorCommand { get; set; }

    public NavigatorViewModel(IRegionManager regionManager)
    {
        _RegionManager = regionManager;
        NavigatorCommand = new DelegateCommand<string>(Navigation);
    }

    private void Navigation(string viewName) => _RegionManager?.RequestNavigate("ViewRegion", viewName, NavigationResult);

    private void NavigationResult(NavigationResult result)
    {
    }
}
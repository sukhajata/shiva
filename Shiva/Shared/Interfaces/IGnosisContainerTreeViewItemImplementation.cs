using Shiva.Shared.BaseControllers;
using Shiva.Shared.ContainerControllers;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisContainerTreeViewItemImplementation
    {
        string Caption { get; set; }
        string GnosisIcon { get; set; }
        GnosisVisibleController GnosisTag { get; set; }

        //void SetCaption(string caption);
        //void SetIconName(string iconName);
        void AddItem(IGnosisContainerTreeViewItemImplementation item);
       // void SetTag(GnosisVisibleController controller);
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace C4I
{
    /// <summary>
    /// Specialized tree view
    /// </summary>
    public class EddieTreeView : TreeView
    {
        static EddieTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieTreeView), new FrameworkPropertyMetadata(typeof(EddieTreeView)));
        }

        /// 
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is EddieTreeViewItem;
        }

        ///
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new EddieTreeViewItem();
        }

        /// 
        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
            base.OnManipulationBoundaryFeedback(e);
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <summary> 
    /// A child of a <see cref="EddieTreeView" />.
    /// </summary> 
    public class EddieTreeViewItem : TreeViewItem
    {
        static EddieTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieTreeViewItem), new FrameworkPropertyMetadata(typeof(EddieTreeViewItem)));
        }

        /// <summary>
        /// Dependency property to select the item size
        /// </summary>
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
            "ItemHeight", typeof(ControlSize), typeof(EddieTreeViewItem), new PropertyMetadata(ControlSize.Large));

        /// <summary>
        /// Property for the selected size of the item
        /// </summary>
        public ControlSize ItemHeight
        {
            get { return (ControlSize)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        /// <inheritdoc />
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new EddieTreeViewItem();
        }

        /// <inheritdoc />
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is EddieTreeViewItem;
        }
    }
}
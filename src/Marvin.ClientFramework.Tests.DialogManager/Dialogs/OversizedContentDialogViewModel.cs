using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Marvin.ClientFramework.Commands;
using Marvin.ClientFramework.Dialog;

namespace Marvin.ClientFramework.Tests.DialogManager
{
    public class TreeItemModel
    {
        public string DisplayName { get; set; }

        public List<TreeItemModel> Children { get; } = new List<TreeItemModel>();
    }

    public class OversizedContentDialogViewModel : DialogScreen
    {
        public ICommand CancelCmd { get; private set; }

        public List<TreeItemModel> TreeItemModels { get; } = new List<TreeItemModel>();

        private TreeItemModel _selectedItem;
        public TreeItemModel SelectedItem
        {
            get { return _selectedItem; }
            private set
            {
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                NotifyOfPropertyChange();
            }
        }

        protected override void OnInitialize()
        {
            DisplayName = "Oversized Content™";

            CreateTree(TreeItemModels, 1);

            CancelCmd = new DelegateCommand(CloseCommand);

            base.OnInitialize();
        }

        public void CloseCommand(object obj)
        {
            TryClose(true);
        }

        private void CreateTree(List<TreeItemModel> treeModels, int counter)
        {
            for (var idx = counter; idx <= 20; idx++)
            {
                var newTreeItemModel = new TreeItemModel { DisplayName = $"Item {idx}"};
                CreateTree(newTreeItemModel.Children, counter + idx);

                treeModels.Add(newTreeItemModel);
            }
        }

        public void OnTreeItemChanged(object sender, RoutedPropertyChangedEventArgs<object> args)
        {
            SelectedItem = (TreeItemModel)args.NewValue;
        }
    }
}

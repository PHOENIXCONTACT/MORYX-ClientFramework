// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Moryx.Controls.Demo.ViewModels;

namespace Moryx.Controls.Demo.Shell
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        public ShellViewModel()
        {
            Items.AddRange(new Screen[]
            {
                new ButtonsViewModel(),
                new ComboBoxesViewModel(),
                new ProgressViewModel(),
                new ItemSelectionViewModel(),
                new TextBoxesViewModel(),
                new PanelsViewModel(),
                new ListViewViewModel(),
                new ListBoxViewModel(),
                new TreeViewViewModel(),
                new TabControlViewModel(),
                new IconsViewModel(),
                new SliderViewModel(),
                new TemplatesViewModel(),
                new NavigationBarViewModel(),
                new EntryEditorViewModel()
            });
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);
            await ActivateItemAsync(Items.First(), cancellationToken);
        }
    }
}

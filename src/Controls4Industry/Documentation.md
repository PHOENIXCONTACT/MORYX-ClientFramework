﻿

Control Library {#controls4Industry}
========

[TOC]

The control library will be build with the client framework an provides several controls implementing the default marvin style. 
You dont need to add a namespace to the controls because all custom controls are added to the systems default style namespace. 
Controls need no addition namespace. All controls will be added to the system namespace, so they are easier to use. 

~~~~{.cs}
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "C4I")]
~~~~

Reference Controls4Industry out of *Libraries* folder from the ClientFramework
The resources will be automatically loaded by the framework. So all used controls in the views will get the default eddie style.

# Buttons {#buttons}
Description missing...

# Item Selection {#itemSelection}
Description missing...

# Shapes {#shapes}
Shapes generally can be loaded with the [ShapeFactory](@ref C4I.ShapeFactory). The ShapeFactory will return the PathGeometry static directly.
In the enum [CommonShapeType](@ref C4I.CommonShapeType), all icons are defined.

~~~~{.cs}
var myGeometry = ShapeFactory.GetShapeGeometry(CommonShapeType.Pencil)
~~~~

## XAML Extensions {#shapeXamlExtension}
For the ShapeFactory XAML extension [CommonShapeExtension](@ref C4I.CommonShapeExtension) will load the resource statically to the XAML part.

~~~~{.xaml}
<Path Data="{CommonShape ShapeType=Cross}" />
~~~~

Additionaly some attached XAML properties can be used to get shapes to controls. 
As an example, we can use the attached property at a path. This extension will automatically set the data value on the path.

~~~~{.xaml}
<Path Eddie.Icon="Pencil" Width="50" Height="50" Fill="Black"/>
~~~~

The extension can be used on the following controls:

* Path

## Available Shapes {#availableShapes}
Available shapes are documented in the [CommonShapeType](@ref C4I.CommonShapeType) enum.

**Short list:** Unset, ArrowUp, ArrowUpInCycle, ArrowDown, ArrowDownInCycle, ArrowRight, ArrowLeft, Lock, Ok, Cross, CheckMark, Info, AttentionTriangle, ExclamationMark, FullScreen, Magnifier, Pencil, PxCLogo, QuestionMark, SpeakBubble, Plus, Minus, Printer, Undo, Redo, Cloud, Symbol, Copy, Refresh, User, Key, Service, Cells, Gear, Process, Open, Save, Paste, Cut, Properties, New, Camera, ArrowRight2, Delete, Home, BarChart, Heartrate, Binocular, BinocularCrossed, ClockWithCheckmark, Document, SignedDocument, Pause,  

# ListBox / ListView {#lists}
The framework provides a posibility to sort listviews with a simple attached property class called 'GridViewSort'.
The GridViewSort is part of the C4I.

__How to use the GridViewSort (ListView sort)__
The sort can beused in two differnt ways: auto-sort or manual-sort.
For the auto-sort you must simply set the AutoSort-property to true. The GridViewSort.AutoSort-Property must be written to the listview itself.
~~~~{.xaml}
<ListView GridViewSort.AutoSort="True" >
~~~~
Then you must set the PropertyName-property foreach column you would like to have sortable. The value of the PropertyName-property must be the same like the binded property name.
the GridView.PropertyName -Property must be written to a GridViewColumn.
~~~~{.xaml}
<GridViewColumn DisplayMemberBinding="{Binding Id}" GridViewSort.PropertyName="Id">
~~~~

For the manual sort you must provide commands for sorting. You must add the GridViewSort.Command-property to the listview itself.
On header click the GridViewSort will invoke the binded command with the name of the property (set in the GridViewsort.PropertyName-property) as parameter.
The command must sort the content of the list. 
The manual sorting using the command will override the auto-sorting.

The GridViewSort allows you to change the sort icon by setting the SortGlyphAscending- or SortGlyphDescending-property. By default the sort glyph is a small triangle.
These propertys are also placed in the listview itself.

Tipp:
If you have a right alligned header you must add a 13px right site margin to the header. Otherwise the sort-adorner will overlay the header text.
~~~~{.xaml}
<GridViewColumn DisplayMemberBinding="{Binding Id}" GridViewSort.PropertyName="Id">
    <GridViewColumnHeader HorizontalContentAlignment="Right">
        <TextBlock Text="Id" Margin="0,0,13,0"/>
    </GridViewColumnHeader>
</GridViewColumn>
~~~~

# Progress Indicator {#progress}
Description missing...

# TabControl {#tabs}
Description missing...

# TreeView {#trees}
Description missing...

# Slider {#slider}
Description missing...

# NotificationBar {#notificationBar}
Description missing...

# Special Controls {#special}
Description missing...

# Value Converter {#converter}
The following converters are awailable in the control library and they are provide a way to apply custom logic to a binding.
Click on the converter to get detailed explanation and code documentation.

* [BooleanToGridRowHeightConverter](@ref C4I.BooleanToGridRowHeightConverter)
* [BooleanToObjectConverter](@ref C4I.BooleanToObjectConverter)
* [BooleanToVisibilityConverter](@ref C4I.BooleanToVisibilityConverter)
* [CombiningConverter](@ref C4I.CombiningConverter)
* [DoubleToRowDefinitionConverter](@ref C4I.DoubleToRowDefinitionConverter)
* [FileSizeToTextConverter](@ref C4I.FileSizeToTextConverter)
* [GenericEnumConverter](@ref C4I.GenericEnumConverter)
* [InverseBooleanConverter](@ref C4I.InverseBooleanConverter)
* [IntToBooleanConverter](@ref C4I.IntToBooleanConverter)
* [NullToCollapsedConverter](@ref C4I.NullToCollapsedConverter)
* [StringToBooleanConverter](@ref C4I.StringToBooleanConverter)
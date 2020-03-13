// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace C4I
{
    internal class SortGlyphAdorner : Adorner
    {
        private readonly GridViewColumnHeader _columnHeader;
        private readonly ListSortDirection _direction;
        private readonly ImageSource _sortGlyph;

        public SortGlyphAdorner(GridViewColumnHeader columnHeader, ListSortDirection direction, ImageSource sortGlyph)
            : base(columnHeader)
        {
            _columnHeader = columnHeader;
            _direction = direction;
            _sortGlyph = sortGlyph;
        }

        private Geometry GetDefaultGlyph()
        {
            var x1 = _columnHeader.ActualWidth - 13;
            var x2 = x1 + 10;
            var x3 = x1 + 5;
            var y1 = _columnHeader.ActualHeight / 2 - 3;
            var y2 = y1 + 5;

            if (_direction == ListSortDirection.Ascending)
            {
                var tmp = y1;
                y1 = y2;
                y2 = tmp;
            }

            var pathSegmentCollection = new PathSegmentCollection
            {
                new LineSegment(new Point(x2, y1), true),
                new LineSegment(new Point(x3, y2), true)
            };

            var pathFigure = new PathFigure(new Point(x1, y1), pathSegmentCollection, true);

            var pathFigureCollection = new PathFigureCollection();
            pathFigureCollection.Add(pathFigure);

            var pathGeometry = new PathGeometry(pathFigureCollection);
            return pathGeometry;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (_sortGlyph != null)
            {
                var x = _columnHeader.ActualWidth - 13;
                var y = _columnHeader.ActualHeight / 2 - 5;
                var rect = new Rect(x, y, 10, 10);
                drawingContext.DrawImage(_sortGlyph, rect);
            }
            else
            {
                drawingContext.DrawGeometry(Brushes.LightGray, new Pen(Brushes.Gray, 1.0), GetDefaultGlyph());
            }
        }
    }
}

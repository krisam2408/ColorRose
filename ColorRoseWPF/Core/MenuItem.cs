﻿using System.Windows.Media;

namespace ColorRoseWPF.Core
{
    public class MenuItem : ListItem
    {
        public Brush BackgroundColor { get; set; }
        public Brush TextColor { get; set; }

        public MenuItem(int key, string display, bool selected = false):base(key, display, selected) { }
    }
}

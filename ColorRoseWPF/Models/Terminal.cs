using ColorRoseWPF.Core;
using ColorRoseWPF.ViewModels;

namespace ColorRoseWPF.Models
{
    public class Terminal
    {
        public int WindowWidth { get; private set; }
        public int WindowHeight { get; private set; }

        public NavigationViewModel Navigation { get; set; }

        [LandingPage("SingleColorPage", "Single Color", isSelected: true)]
        [ButtonSettings(null, "Red", "White")]
        public SingleColorViewModel SingleColor { get; set; }

        [LandingPage("ColorGalleryPage", "Gallery")]
        [ButtonSettings(null, "#00F", "White")]
        public ColorGalleryViewModel ColorGallery { get; set; }

        [LandingPage("HarmoniesPage", "Color Wheel", isEnabled: false)]
        public HarmoniesViewModel Harmonies { get; set; }

        private static Terminal instance;
        public static Terminal Instance
        {
            get
            {
                if (instance == null)
                    instance = new();
                return instance;
            }
        }

        private Terminal()
        {
            WindowHeight = 450;
            WindowWidth = 800;
        }
    }
}

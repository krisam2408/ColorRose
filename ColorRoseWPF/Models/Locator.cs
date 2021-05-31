namespace ColorRoseWPF.Models
{
    public class Locator
    {
        public Terminal Terminal { get; private set; }

        public Locator() { Terminal = Terminal.Instance; }
    }
}

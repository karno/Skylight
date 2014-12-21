
namespace Skylight.Messaging
{
    public class TextBoxCaretMessage : MessageBase
    {
        public TextBoxCaretMessage()
        {
        }

        public TextBoxCaretMessage(int start)
        {
            SelectionStart = start;
        }

        public TextBoxCaretMessage(int start, int length)
        {
            SelectionStart = start;
            SelectionLength = length;
        }

        public TextBoxCaretMessage(string replacing)
        {
            SelectionReplacingText = replacing;
        }

        public TextBoxCaretMessage(string key, int start)
            : base(key)
        {
            SelectionStart = start;
        }

        public TextBoxCaretMessage(string key, int start, int length)
            : base(key)
        {
            SelectionStart = start;
            SelectionLength = length;
        }

        public TextBoxCaretMessage(string key, string replacing)
            : base(key)
        {
            SelectionReplacingText = replacing;
        }

        public int? SelectionStart { get; set; }

        public int? SelectionLength { get; set; }

        public string SelectionReplacingText { get; set; }
    }
}

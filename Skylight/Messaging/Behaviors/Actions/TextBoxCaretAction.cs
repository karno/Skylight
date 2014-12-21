using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Skylight.Messaging.Behaviors.Actions
{
    public class TextBoxCaretAction : MessageActionBase<TextBoxCaretMessage>
    {
        protected override bool Execute(DependencyObject associatedObject, TextBoxCaretMessage message)
        {
            var textBox = associatedObject as TextBox;
            if (textBox == null) return false;
            try
            {
                if (message.SelectionStart != null)
                {
                    textBox.SelectionStart = message.SelectionStart.Value;
                }
                if (message.SelectionLength != null)
                {
                    textBox.SelectionLength = message.SelectionLength.Value;
                }
                if (message.SelectionReplacingText != null)
                {
                    textBox.SelectedText = message.SelectionReplacingText;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

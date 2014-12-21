using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;

namespace Skylight.Messaging
{
    public sealed class MessageDialogMessage : ResponsiveMessageBase<MessageDialogMessageResult>
    {
        private readonly List<IUICommand> _commands = new List<IUICommand>();

        public IList<IUICommand> Commands
        {
            get { return _commands; }
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public int? DefaultCommandIndex { get; set; }

        public int? CancelCommandIndex { get; set; }

        public MessageDialogOptions Options { get; set; }

        public MessageDialogMessage()
        {
            Title = null;
            Content = null;
            DefaultCommandIndex = null;
            CancelCommandIndex = null;
            Options = MessageDialogOptions.None;
        }

        public MessageDialogMessage(string content)
            : this()
        {
            Content = content;
        }

        public MessageDialogMessage(string content, string title)
            : this(content)
        {
            Title = title;
        }

        public MessageDialogMessage(string content, string title, IEnumerable<IUICommand> commands)
            : this(content, title)
        {
            _commands = commands.ToList();
        }

        public MessageDialogMessage(string content, string title, params IUICommand[] commands)
            : this(content, title, (IEnumerable<IUICommand>)commands)
        {
        }

        public MessageDialogMessage(string key, string content, string title)
            : base(key)
        {
            Title = title;
            Content = content;
            DefaultCommandIndex = null;
            CancelCommandIndex = null;
            Options = MessageDialogOptions.None;
        }

        public MessageDialogMessage(string key, string content, string title, IEnumerable<IUICommand> commands)
            : this(key, content, title)
        {
            _commands = commands.ToList();
        }

        public MessageDialogMessage(string key, string content, string title, params IUICommand[] commands)
            : this(key, content, title, (IEnumerable<IUICommand>)commands)
        {
        }

    }

    public sealed class MessageDialogMessageResult
    {
        private readonly IUICommand _result;
        private readonly int _resultIndex;

        public MessageDialogMessageResult(IUICommand result, int resultIndex)
        {
            this._result = result;
            this._resultIndex = resultIndex;
        }

        public IUICommand Result
        {
            get { return this._result; }
        }

        public int ResultIndex
        {
            get { return this._resultIndex; }
        }
    }
}

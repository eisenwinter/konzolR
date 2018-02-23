using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.TitleScreen
{
    class DefaultTitleScreen : ITitleScreen
    {
        private string _applicationName;

        private const char _top_left_corner = '╔';
        private const char _top_right_corner = '╗';
        private const char _bottom_right_corner = '╝';
        private const char _bottom_left_corner = '╚';
        private const char _vertical = '║';
        private const char _horizontal = '═';

        private int _messagePos = 0;
     

        public DefaultTitleScreen(string applicationName)
        {
            _applicationName = applicationName;
        }

        private void DrawBox()
        {
            int len = Console.BufferWidth - _applicationName.Length;
            if(len < 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(new String(_horizontal, Console.BufferWidth-1));
                sb.AppendLine();
                sb.Append(_applicationName);
                sb.AppendLine();
                sb.Append(new String(_horizontal, Console.BufferWidth - 1));
                Console.Write(sb);
            }
            else
            {
                int marginLeft = (int)Math.Floor(len * 0.5);
                StringBuilder sb = new StringBuilder();
                sb.Append(new String(' ', marginLeft));
                sb.Append(_top_left_corner);
                sb.Append(new String(_horizontal, _applicationName.Length + 2));
                sb.Append(_top_right_corner);
                sb.AppendLine();
                sb.Append(new String(' ', marginLeft));
                sb.Append(_vertical + " " + _applicationName.PadRight(_applicationName.Length + 1) + _vertical);
                sb.AppendLine();
                sb.Append(new String(' ', marginLeft));
                sb.Append(_bottom_left_corner);
                sb.Append(new String(_horizontal, _applicationName.Length + 2));
                sb.Append(_bottom_right_corner);
                Console.Write(sb);
                Console.WriteLine();
            }
            _messagePos = Console.CursorTop;
        }

        public void ChangeText(string message)
        {
            Console.SetCursorPosition(0, _messagePos);
            Console.WriteLine(message.PadRight(Console.BufferWidth));
        }

        public void Close()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        public void Dispose()
        {
            
        }

        public void Show()
        {
            if (!Console.IsOutputRedirected)
            {
                DrawBox();
            }
            else
            {
                Console.WriteLine(_applicationName);
            }
         
        }
    }
}

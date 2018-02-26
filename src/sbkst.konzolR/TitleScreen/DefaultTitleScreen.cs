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
                sb.Append(new String(AsciiArtIndex.BOX_HORIZONTAL, Console.BufferWidth-1));
                sb.AppendLine();
                sb.Append(_applicationName);
                sb.AppendLine();
                sb.Append(new String(AsciiArtIndex.BOX_HORIZONTAL, Console.BufferWidth - 1));
                Console.Write(sb);
            }
            else
            {
                int marginLeft = (int)Math.Floor(len * 0.5);
                StringBuilder sb = new StringBuilder();
                sb.Append(new String(' ', marginLeft));
                sb.Append(AsciiArtIndex.BOX_TOP_LEFT_CORNER);
                sb.Append(new String(AsciiArtIndex.BOX_HORIZONTAL, _applicationName.Length + 2));
                sb.Append(AsciiArtIndex.BOX_TOP_RIGHT_CORNER);
                sb.AppendLine();
                sb.Append(new String(' ', marginLeft));
                sb.Append(AsciiArtIndex.BOX_VERTICAL + " " + _applicationName.PadRight(_applicationName.Length + 1) + AsciiArtIndex.BOX_VERTICAL);
                sb.AppendLine();
                sb.Append(new String(' ', marginLeft));
                sb.Append(AsciiArtIndex.BOX_BOTTOM_LEFT_CORNER);
                sb.Append(new String(AsciiArtIndex.BOX_HORIZONTAL, _applicationName.Length + 2));
                sb.Append(AsciiArtIndex.BOX_BOTTOM_RIGHT_CORNER);
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

using System;
using System.Collections.Generic;

namespace TinyLogger
{
    public class Logger
    {
        public bool UseLogFile{get;set;}
        public string LogFilePath{get;set;}
        public Action<string> OutputChannel;
        public int Width{get;set;} = 75;

        private Dictionary<ConsoleLevel, ConsoleColor> _colors = new Dictionary<ConsoleLevel, ConsoleColor>
                                                                                                            {
                                                                                                                {ConsoleLevel.Error, ConsoleColor.Red},
                                                                                                                {ConsoleLevel.Warning, ConsoleColor.Yellow},
                                                                                                                {ConsoleLevel.Info, ConsoleColor.Blue},
                                                                                                                {ConsoleLevel.Message, ConsoleColor.White},
                                                                                                                {ConsoleLevel.Success, ConsoleColor.Green},
                                                                                                            };



        public void Log(string message) => HandleLog(message);
        public void Log(string message, ConsoleLevel level) => HandleLog(message, level);


        public void Log(object sender, string message) => HandleLog($"[{sender.GetType().Name}]: {message}");
        public void Log(object sender, string message, ConsoleLevel level) => HandleLog($"[{sender.GetType().Name}]: {message}", level);


        public void Log(object sender, object message) => HandleLog($"[{sender.GetType().Name}]: {message}");
        public void Log(object sender, object message, ConsoleLevel level) => HandleLog($"[{sender.GetType().Name}]: {message}", level);


        public void Seperator()
        {
            HandleLog(new string('-', Width));
        }
        public void BlankLine()
        {
            HandleLog(" ");
        }
        public void CommentSeperator()
        {
            string _str = $"//{new string('-', Width - 4)}//";
            HandleLog(_str);
        }
        public void Surround(string message)
        {
            int halfWidth = ((Width - message.Length) / 2) - 2;
            CommentSeperator();
            string _str = $"//{new string('-', halfWidth)}{message}{new string('-', halfWidth)}//";
            HandleLog(_str);
            CommentSeperator();
        }
        public void Surround(List<string> messages)
        {
            CommentSeperator();

            foreach (var message in messages)
            {
                int halfWidth = ((Width - message.Length) / 2) - 2;
                string _str = $"//{new string('-', halfWidth)}{message}{new string('-', halfWidth)}//";
                HandleLog(_str);              
            }

            CommentSeperator();
        }


        private void HandleLog(string _message, ConsoleLevel level = ConsoleLevel.Message)
        {
            if(UseLogFile)
            {
                File.AppendAllText(LogFilePath, _message + Environment.NewLine);
                return;
            }

            OutputChannel?.Invoke(_message);

            Console.ForegroundColor = GetColor(level);
            Console.WriteLine(_message);
            Console.ResetColor();
        }
        private ConsoleColor GetColor(ConsoleLevel level) =>  _colors[level];
    }


    public enum ConsoleLevel
    {
        Error,
        Warning,
        Message,
        Success,
        Info,
    };

}



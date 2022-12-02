using System;
using System.Text;

namespace TinyLogger
{
    public class Table
    {
        private List<string> _header = new List<string>();
        private List<List<string>> _rows = new List<List<string>>();
        public ConsoleLevel ConsoleLevel{get;set;} = ConsoleLevel.Message;

        private Logger _logger; 

        public Table(Logger _logger)
        {
            this._logger = _logger;
        }

        public void AddHeader(List<string> _head) => _header = _head;
        public void AddRow(List<string> _row) => _rows.Add(_row);
        public void Output()
        {
            StringBuilder _sb = new StringBuilder();

            if(!_hasHeader)
            {
                Console.WriteLine("----------Table Does not have Header----------");
                return;
            }

            _sb.Append(PrintLine());

            _sb.Append(PrintRow(_header));

            _sb.Append(PrintLine());

            if(!_hasRows){return;}

            foreach (var item in _rows)
            {
                _sb.Append(PrintRow(item));
            }
            
            _sb.Append(PrintLine());
            _logger.Log(_sb.ToString(), this.ConsoleLevel);
        }
        public void ClearTable()
        {
            ClearHeader();
            ClearRows();
        }        
        public void ClearHeader() => _header.Clear();
        public void ClearRows() => _rows.Clear();

        private bool _hasHeader => _header.Count > 0;
        private bool _hasRows => _rows.Count > 0;



        private string PrintLine()
        {
            return new string('-', _logger.Width) + Environment.NewLine;
        }

        private string PrintRow(List<string> columns)
        {
            int width = (_logger.Width - columns.Count) / columns.Count;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            return row + Environment.NewLine;
        }

        private string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}



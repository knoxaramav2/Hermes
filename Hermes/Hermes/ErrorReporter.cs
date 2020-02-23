using System;
using System.Collections.Generic;
using System.Text;

namespace Hermes
{
    public class ErrorReporter
    {
        private static ErrorReporter _errReporter;

        private List<Tuple<int, string>> _errList;
        private ErrorReporter()
        {
            _errList = new List<Tuple<int, string>>();
        }

        public static void Init()
        {
            if (_errReporter == null) { return; }
            _errReporter = new ErrorReporter();
        }

        public static void AddError()
        {

        }
    
        public enum ErrorCodes
        {

        }        
    }
}

using System.Collections.Generic;

namespace Shop_App_Sql_WPF
{
    internal class InfoLog
    {
        public List<string> log = new();
        public void AddToLog(string msg) => log.Add(msg);
    }
}

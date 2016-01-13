using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCo.ConsoleApp.Utilities
{
    public interface IReportHandler
    {
        void SetupColumns(params int[] columnSizes);
        void AddRow();
        void AddColumns(params string[] values);
        void AddColumns(char paddingChar, params string[] values);
        void AddColumn(char paddingChar, string value);
        void AddColumn(string value);
        void GenerateReport();
    }
}

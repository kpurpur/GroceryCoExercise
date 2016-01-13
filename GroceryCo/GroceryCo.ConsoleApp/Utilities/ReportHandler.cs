using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;

namespace GroceryCo.ConsoleApp.Utilities
{
    public class ReportHandler : IReportHandler
    {
        #region Private Variables

        private List<string> rows = new List<string>();

        private int[] columnSizes;
        private int colSpacerWidth = 2;
        private int currentColumn = -1;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets up the column sizes for a row
        /// </summary>
        /// <param name="columnSizes"></param>
        public void SetupColumns(params int[] columnSizes)
        {
            this.columnSizes = columnSizes;
        }

        /// <summary>
        /// Adds a row to the report
        /// </summary>
        public void AddRow()
        {
            rows.Add("");
            currentColumn = -1;
        }

        /// <summary>
        /// Adds a row and values for the columns
        /// </summary>
        /// <param name="values"></param>
        public void AddColumns(params string[] values)
        {
            AddColumns(' ', values);
        }

        /// <summary>
        /// Adds a row and values for the columns
        /// </summary>
        /// <param name="paddingChar"></param>
        /// <param name="values"></param>
        public void AddColumns(char paddingChar, params string[] values)
        {
            AddRow();
            foreach (string value in values)
                AddColumn(paddingChar, value);
        }

        /// <summary>
        /// Adds a column to the current row
        /// </summary>
        /// <param name="value"></param>
        public void AddColumn(string value)
        {
            AddColumn(' ', value);
        }

        /// <summary>
        /// Adds a column to the current row
        /// </summary>
        /// <param name="paddingChar"></param>
        /// <param name="value"></param>
        public void AddColumn(char paddingChar, string value)
        {
            string currentRow = rows[rows.Count - 1];
            currentColumn++;

            if (currentRow != "") currentRow += "".PadRight(colSpacerWidth, ' ');

            currentRow += value.PadRight(columnSizes[currentColumn], paddingChar);

            rows[rows.Count - 1] = currentRow;
        }

        /// <summary>
        /// Generates the report to the console
        /// </summary>
        public void GenerateReport()
        {
            foreach (string row in rows)
                Console.WriteLine(row);
        }

        #endregion

    }
}

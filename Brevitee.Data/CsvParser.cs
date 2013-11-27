using System;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;

namespace Naizari.Data.Mapping
{

    /*Like the CsvWriter class, all methods in the CsvParser class are static.
     * There are four parser methods that return a DataTable from either a string
     * or text stream, and expecting a header line in the CSV source or not. Only
     * one method has a real body, all others just adjust their parameter signature.
     * The implementation is simple: the method reads one "row" of the CSV source at
     * a time and populates a row in the DataTable. The real meat is the private class
     * CsvStream explained below. There is one utility method that returns an unused
     * column name for a DataTable, in case there are no headers in the CSV source or
     * the headers are not unique.
    */



    public sealed class CsvParser
    {

        public static DataTable Parse(CsvFileParserOptions Options)
        {
            if (Options.AutoDetectCsvDelimiter)
            {
                Options.CsvDelimiter = CsvUtils.DetectFieldDelimiterChar(Options.Filename, Options.Encoding);
            }
            return Parse(new StreamReader(Options.Filename, Options.Encoding), Options.HeadersAction, Options.CsvDelimiter);
        }

        //By Filename
        public static DataTable Parse(string filename, System.Text.Encoding Encoding, bool headers, char delimiter)
        {
            return Parse(new StreamReader(filename, Encoding), headers, delimiter);
        }

        public static DataTable Parse(string filename, System.Text.Encoding Encoding, bool headers)
        {
            return Parse(filename, Encoding, true, CsvUtils.DetectFieldDelimiterChar(filename, Encoding));
        }

        //By String
        public static DataTable Parse(string data, bool headers, char delimiter)
        {
            return Parse(new StringReader(data), headers, delimiter);
        }

        public static DataTable Parse(string data, char delimiter)
        {
            return Parse(new StringReader(data), delimiter);
        }

        public static DataTable Parse(TextReader stream, char delimiter)
        {
            return Parse(stream, false, delimiter);
        }

        public static DataTable Parse(TextReader stream, bool Headers, char delimiter)
        {
            if (Headers)
                return Parse(stream, HeadersAction.UseAsColumnNames, delimiter);
            else
                return Parse(stream, HeadersAction.UseAsContent, delimiter);
        }

        public static DataTable Parse(TextReader stream, HeadersAction Headers, char delimiter)
        {
            DataTable table = new DataTable();
            CsvStream csv = new CsvStream(stream);
            string[] row = csv.GetNextRow(delimiter);
            if (row == null)
                return null;
            if (Headers == HeadersAction.SkipHeaderLine)
            {
                row = csv.GetNextRow(delimiter);
            }
            if (Headers == HeadersAction.UseAsColumnNames)
            {
                //foreach (string header in row)
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] != null && row[i].Length > 0 && !table.Columns.Contains(row[i]))
                        table.Columns.Add(row[i], typeof(string));
                    else
                        table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                }
                row = csv.GetNextRow(delimiter);
            }
            while (row != null)
            {
                while (row.Length > table.Columns.Count)
                    table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                table.Rows.Add(row);
                row = csv.GetNextRow(delimiter);
            }
            stream.Close();
            stream.Dispose();
            return table;
        }

        private static string GetNextColumnHeader(DataTable table)
        {
            int c = 1;
            while (true)
            {
                string h = "Column" + c++;
                if (!table.Columns.Contains(h))
                    return h;
            }
        }

        /* The CsvStream class does the actual work - read the CSV source in one character at
         * a time and return meaningful chunks of decoded data, namely data items and rows.
        */
        private class CsvStream
        {
            private TextReader stream;

            public CsvStream(TextReader s)
            {
                stream = s;
            }

            public string[] GetNextRow(char delimiter)
            {
                ArrayList row = new ArrayList();
                while (true)
                {
                    string item = GetNextItem(delimiter);
                    if (item == null)
                        return row.Count == 0 ? null : (string[])row.ToArray(typeof(string));
                    row.Add(item);
                }
            }

            private bool EOS = false;
            private bool EOL = false;

            private string GetNextItem(char delimiter)
            {
                if (EOL)
                {
                    // previous item was last in line, Start new line
                    EOL = false;
                    return null;
                }

                bool quoted = false;
                bool predata = true;
                bool postdata = false;
                StringBuilder item = new StringBuilder();

                while (true)
                {
                    char c = GetNextChar(true);
                    if (EOS)
                    {
                        return item.Length > 0 ? item.ToString() : null;
                    }

                    if ((postdata || !quoted) && c == delimiter)
                    {
                        // End of item, return
                        return item.ToString();
                    }

                    if ((predata || postdata || !quoted) && (c == '\x0A' || c == '\x0D'))
                    {
                        // we are at the End of the line, eat newline characters and exit
                        EOL = true;
                        if (c == '\x0D' && GetNextChar(false) == '\x0A')
                        {
                            // new line sequence is 0D0A
                            GetNextChar(true);
                        }
                        return item.ToString();
                    }

                    if (predata && c == ' ')
                    {
                        // whitespace preceeding data, discard
                        continue;
                    }

                    if (predata && c == '"')
                    {
                        // quoted data is starting
                        quoted = true;
                        predata = false;
                        continue;
                    }

                    if (predata)
                    {
                        // data is starting without quotes
                        predata = false;
                        item.Append(c);
                        continue;
                    }

                    if (c == '"' && quoted)
                    {
                        if (GetNextChar(false) == '"')
                            // double quotes within quoted string means add a quote       
                            item.Append(GetNextChar(true));
                        else
                            // End-quote reached
                            postdata = true;
                        continue;
                    }

                    // all cases covered, character must be data
                    item.Append(c);
                }
            }

            private char[] buffer = new char[4096];
            private int pos = 0;
            private int length = 0;

            private char GetNextChar(bool eat)
            {
                if (pos >= length)
                {
                    length = stream.ReadBlock(buffer, 0, buffer.Length);
                    if (length == 0)
                    {
                        EOS = true;
                        return '\0';
                    }
                    pos = 0;
                }
                if (eat)
                {
                    return buffer[pos++];
                }
                else
                {
                    return buffer[pos];
                }
            }
        }
    }
}


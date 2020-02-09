using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace CSVManager
{
    public class CSVReader
    {
        public static List<string[]> CSVRead(TextAsset musicscore)
        {
            List<string[]> csvdata = new List<string[]>();

            StringReader reader = new StringReader(musicscore.text);

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                csvdata.Add(line.Split(','));
            }

            return csvdata;
        }


        public static List<string[]> CSVRead(string filename)
        {
            List<string[]> csvdata = new List<string[]>();

            StreamReader reader = new StreamReader(filename);

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                csvdata.Add(line.Split(','));
            }

            return csvdata;
        }
    }

    public class CSVWriter
    {
        public static void CSVWrite(string filepath, List<string[]> data)
        {
            StreamWriter writer = new StreamWriter(filepath, false);

            foreach (string[] notedata in data)
            {
                string joinnotedata = string.Join(",", notedata);
                writer.WriteLine(joinnotedata);
            }

            writer.Close();
        }
    }
}
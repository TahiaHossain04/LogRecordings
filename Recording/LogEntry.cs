// Author:        Tahia Hossain
// Date Created:  31st October 2024
// Date Modified: 1st November 2024
// Description:   ?

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Recording
{
    class LogEntry
    {
        // Static variables
        private static int count = 0;
        private static DateTime firstEntry;
        private static DateTime newestEntry;

        // Instance variables
        private int logID;
        private DateTime logDate = DateTime.Now;
        private int logWellness;
        private int logQuality;
        private string logNotes = String.Empty;
        private FileInfo logFile = new FileInfo("");

        // Constructors

        // Default sets the count and id values
        public LogEntry()
        {
            count++;
            logID = count;
        }

        // Parameterized consturctor to create a new log entry object
        // wellnessValye between 1 and 5
        // qualityValue between 1 and 5
        // Notes for this log entry
        // File path associated


        public LogEntry(int wellnessValue, int qualityValue, string notesValue, FileInfo filevalue)
        {
            // Update the static variables
            count++;
            if (count == 0)
            {
                firstEntry = DateTime.Now;
            }
            newestEntry = DateTime.Now;


            logID = count;
            Wellness = wellnessValue;
            Quality = qualityValue;
            Notes = notesValue;
            RecordingFile = filevalue;
        }

        // Properties

        public int Id
        {
            get => logID; private set
            {
                logID = value;
            }

        }

        public DateTime EntryDate
        { get => logDate; set
            {
                logDate = value;
            }
        }

        public int Wellness
        {
            get => logWellness; set
            {
                logWellness = value;
            }
        }
        public int Quality
        {
            get => logQuality; set
            {
                logQuality = value;
            }
        }

        public string Notes
        {
            get => logNotes; set
            {
                if (value.Trim().Length > 0)
                {
                    logNotes = value;
                }
                else
                {
                    MessageBox.Show("The notes have been left blank.", "Entry Error");
                    logNotes = value;
                }
            }
        }

        public FileInfo RecordingFile
        {
            get => logFile; set
            {
                logFile = value;
            }
        }

        // Get the total number of log entries

        public static int Count => count;

        // Get the first log entry's data

        public static DateTime FirstEntry => firstEntry;

        // Get the most recent log entry's data

        public static DateTime NewestEntry => newestEntry;

        // Displays a log entry as a string
        // Logs entry as a string
        public override string ToString()
        {
            return "Entry" + Id + "created at" + EntryDate.ToString();
        }
    }
}

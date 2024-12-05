// Author:        Tahia Hossain
// File:          Recording (version 2)
// Date Created:  13th November 2024
// Date Modified: 16th November 2024
// Description:   A class created to keep track of the entered logs 

using System;
using System.Collections.Generic;

namespace Recording
{
    [Serializable]
    public abstract class LogEntry
    {
        // Static list to hold all saved entries
        public static List<LogEntry> logEntries = new List<LogEntry>();

        // Static variables
        public static int count = 0;
        public static DateTime firstEntry;
        public static DateTime newestEntry;

        // Instance variables
        public int logID;
        public DateTime logDate = DateTime.Now;
        public int logWellness;
        public int logQuality;
        public string logNotes = string.Empty;

        // Properties
        public int Id
        {
            get => logID;
            set => logID = value;
        }

        public DateTime EntryDate
        {
            get => logDate;
            set => logDate = value;
        }

        public int Wellness
        {
            get => logWellness;
            set => logWellness = value;
        }

        public int Quality
        {
            get => logQuality;
            set => logQuality = value;
        }

        public string Notes
        {
            get => logNotes;
            set => logNotes = value;
        }

        public static int Count => count;
        public static DateTime FirstEntry => firstEntry;
        public static DateTime NewestEntry => newestEntry;
    }
}



// Author:        Tahia Hossain
// File:          Recording (version 2)
// File:          Recording (version 3)
// Date Created:  29th November 2024
// Date Modified: 05th December 2024
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

        // Unique Log ID for each entry
        public int Id
        {
            get => logID;
            set => logID = value;
        }

        // First Entry Created Date
        public DateTime EntryDate
        {
            get => logDate;
            set => logDate = value;
        }

        // Wellness Combobox Index Selected
        public int Wellness
        {
            get => logWellness;
            set => logWellness = value;
        }

        // Quality Combobox Index Selected
        public int Quality
        {
            get => logQuality;
            set => logQuality = value;
        }

        // Notes or Essay 
        public string Notes
        {
            get => logNotes;
            set => logNotes = value;
        }

        // COunting the number of entries
        public static int Count => count;
        // First Entry as firstEntry in the date time format
        public static DateTime FirstEntry => firstEntry;
        // Newest Entry as newestEntry in the date time format
        public static DateTime NewestEntry => newestEntry;
    }
}



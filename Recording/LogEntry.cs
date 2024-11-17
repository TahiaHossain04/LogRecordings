// Author:        Tahia Hossain
// File:          Recording (version 2)
// Date Created:  13th November 2024
// Date Modified: 16th November 2024
// Description:   A class created to keep track of the entered logs 

// Imports
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Recording
{
    // New Class is dclared
    internal class LogEntry
    {
        // Static list to hold all saved entries
        protected internal static List<LogEntry> logEntries = new List<LogEntry>();

        // Static variables
        // Tracks the number of LogEntry objects created.
        protected static int count = 0;
        // Tracks the date and time of the first and last entry
        protected static DateTime firstEntry;
        protected static DateTime newestEntry;

        // Instance variables
        // Each LogEntry object will have its own copy
        // Unique ID for every new entry created
        protected int logID;
        // Date and Time set to current time for when the entry was created
        protected DateTime logDate = DateTime.Now;
        protected int logWellness;
        protected int logQuality;
        // An empty string to store notes with the long entry
        protected string logNotes = String.Empty;

        // Constructors
        // Cant instantiate?

        // Properties
        // Allows read-only access to logID
        // Assigning and setting unique id to each entry created (both audio and text)
        protected internal int Id
        {
            get => logID; private set
            {
                logID = value;
            }

        }

        // Provides read and write access to logDate
        // Gets and Sets the DateTime an entry was created
        protected internal DateTime EntryDate
        {
            get => logDate; set
            {
                logDate = value;
            }
        }

        // Read and Write
        // Takes the value from the combobox and allows to set it 
        protected internal int Wellness
        {
            get => logWellness; set
            {
                logWellness = value;
            }
        }

        // Read and Write
        // Takes the value from the combobox and allows to set it 
        protected internal int Quality
        {
            get => logQuality; set
            {
                logQuality = value;
            }
        }

        // Read and Write the string called notes
        // Notes in the Audio Entry Tab
        // Essay Textbox in the Text Entry Tab
        protected internal string Notes
        {
            get => logNotes; set
            {
                logNotes = value;
            }
        }


        // Get the total number of log entries

        protected internal static int Count => count;

        // Get the first log entry's data

        protected internal static DateTime FirstEntry => firstEntry;

        // Get the most recent log entry's data

        protected internal static DateTime NewestEntry => newestEntry;

        // Displays a log entry as a string
        public override string ToString()
        {
            return $"Entry {Id} created at {EntryDate}, Wellness: {Wellness}, Quality: {Quality}, Notes: {Notes}";
        }
    }
}


using System;
using System.Collections.Generic;
using Quest.Activities;

namespace Quest
{
    public static class Globals
    {
        public static List<Message> Messages = new List<Message>();
        public static List<Task> Tasks = new List<Task>();
        public static List<Task> Bonuses = new List<Task>();

        public static string TasksUrl = "";
        public static string QuestName = "";
        public static string TasksJson = "";
        public static List<string> MessagesFilenames = new List<string>();
        public static DateTime StartTime;
        public static DateTime EndTime;

        public static Profile MyProfile = new Profile();
    }
}
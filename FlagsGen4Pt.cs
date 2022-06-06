using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    static class FlagsGen4Pt
    {
        static List<string> s_missingEventFlagsList = new List<string>(4096);

        static bool[] s_eventFlags;

        static void CheckFlag(int flagIdx, string aFlagDetail)
        {
            if (!s_eventFlags[flagIdx])
            {
                s_missingEventFlagsList.Add(aFlagDetail);
            }
        }

        public static void ExportFlags(SaveFile savFile)
        {
            s_eventFlags = (savFile as SAV4).GetEventFlags();
            s_missingEventFlagsList.Clear();

            CheckFlags();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s_missingEventFlagsList.Count; ++i)
            {
                sb.AppendFormat("{0}\n", s_missingEventFlagsList[i]);
            }

            System.IO.File.WriteAllText(string.Format("missing_events_{0}.txt", savFile.Version), sb.ToString());
        }

        static void CheckFlags()
        {
            // Hidden Items
            for (int i = 0; i < 256+28; ++i)
            {
                CheckFlag(0x2DA + i, "FLAG_HIDDEN_ITEM_" + i.ToString("X"));
            }


            // Normal items
            for (int i = 0; i < 256+72; ++i)
            {
                CheckFlag(0x3F6 + i, "FLAG_ITEM_" + i.ToString("X"));
            }


            // Trainers (?? 928)
            /*for (int i = 0; i < 1024; ++i)
            {
                CheckFlag(0x550 + i, "FLAG_BEAT_TRAINER_" + i.ToString("X"));
            }*/
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen9SV : EventFlagsChecker
    {
        static string s_chkdb_res = null;
        Dictionary<ulong, bool> m_blocksStatus;

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_blocksStatus = new Dictionary<ulong, bool>(4000);

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            if (s_chkdb_res == null)
            {
                s_chkdb_res = ReadResFile("chkdb_gen9sv.txt");
            }

            m_flagsSourceInfo["EvtFlags"] = 0;
            m_flagsSourceInfo["ItemFlags"] = 1;
            m_flagsSourceInfo["TRFlags"] = 2;
            m_flagsSourceInfo["-"] = -1;

            ParseChecklist(s_chkdb_res);
        }

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            ulong idx = (uint)evtDetail.EvtId;
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            switch (evtDetail.EvtSource)
            {
                case 0: // EvtFlags
                    isEvtSet = (savEventBlocks.GetBlockSafe((uint)idx).Type == SCTypeCode.Bool2);
                    break;

                case 1: // ItemFlags
                    {
                        var bdata = savEventBlocks.GetBlockSafe(0x2482AD60).Data;
                        using (var ms = new System.IO.MemoryStream(bdata))
                        {
                            using (var reader = new System.IO.BinaryReader(ms))
                            {
                                while (ms.Position < ms.Length)
                                {
                                    ulong id = reader.ReadUInt64();
                                    bool isSet = (reader.ReadUInt64() == 1);

                                    if (id == evtDetail.EvtId && isSet)
                                    {
                                        isEvtSet = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case 2: // TRFlags
                    {
                        var bdata1 = savEventBlocks.GetBlockSafe(0xF018C4AC).Data;
                        var bdata2 = savEventBlocks.GetBlockSafe(0x28E475DE).Data;
                        using (var ms1 = new System.IO.MemoryStream(bdata1))
                        using (var ms2 = new System.IO.MemoryStream(bdata2))
                        {
                            using (var reader = new System.IO.BinaryReader(ms1))
                            {
                                while (ms1.Position < ms1.Length)
                                {
                                    ulong id = reader.ReadUInt64();
                                    bool isSet = (reader.ReadUInt64() == 1);

                                    if (id == evtDetail.EvtId && isSet)
                                    {
                                        isEvtSet = true;
                                        break;
                                    }
                                }
                            }
                            if (!isEvtSet)
                            {
                                using (var reader = new System.IO.BinaryReader(ms2))
                                {
                                    while (ms2.Position < ms2.Length)
                                    {
                                        ulong id = reader.ReadUInt64();
                                        bool isSet = (reader.ReadUInt64() == 1);

                                        if (id == evtDetail.EvtId && isSet)
                                        {
                                            isEvtSet = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }
    }
}

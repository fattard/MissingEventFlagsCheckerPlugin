namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen9LZA : EventFlagsChecker
    {
        static string? s_chkdb_res = null;
        Dictionary<ulong, bool>? m_blocksStatus;

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_blocksStatus = new Dictionary<ulong, bool>(4000);

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            s_chkdb_res ??= ReadResFile("chkdb_gen9lza");

            m_flagsSourceInfo["EvtFlags"] = 0;
            m_flagsSourceInfo["SysFlags"] = 1;
            m_flagsSourceInfo["ItemFlags"] = 2;
            m_flagsSourceInfo["-"] = -1;

            ParseChecklist(s_chkdb_res);
        }

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            ulong idx = (uint)evtDetail.EvtId;
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            switch (evtDetail.EvtSource)
            {
                case 0: // EvtFlags
                    {
                        var bdata = savEventBlocks.GetBlockSafe(0x58505C5E).Data.ToArray();
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

                case 1: // SysFlags
                    {
                        var bdata = savEventBlocks.GetBlockSafe(0xED6F46E7).Data.ToArray();
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

                case 2: // ItemFlags
                    {
                        var bdata = savEventBlocks.GetBlockSafe(0x2482AD60).Data.ToArray();
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

                default:
                    isEvtSet = false;
                    break;
            }

            return isEvtSet;
        }
    }
}

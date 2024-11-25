namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen8SWSH : EventFlagsChecker
    {
        static string? s_chkdb_res = null;

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            s_chkdb_res ??= ReadResFile("chkdb_gen8swsh");

            m_flagsSourceInfo["0"] = 0;
            m_flagsSourceInfo["1"] = 1;
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
                case 0: // Bool blocks
                    isEvtSet = (savEventBlocks.GetBlockSafe((uint)idx).Type == SCTypeCode.Bool2);
                    break;

                case 1: // Hidden Items
                    {
                        // Hidden Item data
                        // [0] - state 0: active
                        //             1: active (respawned)
                        //             2: obtained (will recycle)
                        //             3: obtained (permanently)

                        if (idx < 512)
                        {
                            isEvtSet = savEventBlocks.GetBlockSafe(0x6148F6AC).Data[(int)idx * 4] >= 2;
                        }
                        else if (idx < 1024)
                        {
                            var data = savEventBlocks.GetBlockSafe(0xE479EE37).Data;
                            if (data?.Length == 0x810)
                            {
                                isEvtSet = data[((int)idx - 512) * 4] >= 2;
                            }
                        }
                        else if (idx < 1536)
                        {
                            var data = savEventBlocks.GetBlockSafe(0xE579EFCA).Data;
                            if (data?.Length == 0x810)
                            {

                                isEvtSet = data[((int)idx - 1024) * 4] >= 2;
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

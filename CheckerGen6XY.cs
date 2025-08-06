namespace MissingEventFlagsCheckerPlugin
{
    internal class CheckerGen6XY : EventFlagsChecker
    {
        static string? s_chkdb_res = null;

        const int Src_EventFlags = 0;
        const int Src_WorkArea = 1;
        const int Src_BadgeFlags = 2;

        const int Src_EvtEX = 5;
        const int Src_TrainerEX = 6;
        const int Src_GiftEX = 7;
        const int Src_Dex = 8;

        protected override void InitData(SaveFile savFile)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_chkdb_res = null;
#endif

            s_chkdb_res ??= ReadResFile("chkdb_gen6xy");

            m_flagsSourceInfo["0"] = 0; // TODO: remove this

            m_flagsSourceInfo["EvtFlags"] = Src_EventFlags;
            m_flagsSourceInfo["WorkArea"] = Src_WorkArea;
            m_flagsSourceInfo["BadgeFlags"] = Src_BadgeFlags;
            m_flagsSourceInfo["EvtEX"] = Src_EvtEX;
            m_flagsSourceInfo["TrainerEX"] = Src_TrainerEX;
            m_flagsSourceInfo["GiftEX"] = Src_GiftEX;
            m_flagsSourceInfo["-"] = -1;

            ParseChecklist(s_chkdb_res);
        }

        protected override bool IsEvtSet(EventDetail evtDetail)
        {
            bool isEvtSet = false;
            int idx = (int)evtDetail.EvtId;
            SAV6XY savHelper = (m_savFile as SAV6XY)!;

            var eventWorkHelper = ((IEventFlagProvider37)m_savFile!).EventWork;

            switch (evtDetail.EvtSource)
            {
                case Src_EventFlags:
                    isEvtSet = eventWorkHelper.GetEventFlag(idx);
                    break;

                case Src_WorkArea:
                    {
                        switch (idx)
                        {
                            case 0x07C: // Delivered Prof's Letter to Mom
                                isEvtSet = eventWorkHelper.GetWork(idx) >= 7;
                                break;

                            case 0x07F: // Poké Flute event
                                isEvtSet = eventWorkHelper.GetWork(idx) >= 3;
                                break;

                            case 0x08B: // Coastal Kalos Dex
                                isEvtSet = eventWorkHelper.GetWork(idx) >= 1;
                                break;

                            case 0x0AE: // Kalos Hotels Hiker
                            case 0x0AF: // Kalos Hotels Waiter
                            case 0x0B0: // Kalos Hotels Madame
                            case 0x0B1: // Kalos Hotels Maid
                            case 0x0B3: // Kalos Hotels Backpacker
                                isEvtSet = eventWorkHelper.GetWork(idx) == 5;
                                break;

                            case 0x0B2: // Kalos Hotels Tourist
                                isEvtSet = eventWorkHelper.GetWork(idx) >= 4;
                                break;

                            case 0x0E1: // Berry fields - berry mutation event
                                isEvtSet = eventWorkHelper.GetWork(idx) >= 2;
                                break;

                            default:
                                isEvtSet = false;
                                break;
                        }
                    }
                    break;

                case Src_BadgeFlags:
                    isEvtSet = (savHelper.Badges & (1 << idx)) != 0;
                    break;

                case Src_EvtEX:
                    {
                        switch (idx)
                        {
                            default:
                                isEvtSet = false;
                                break;
                        }
                    }
                    break;

                case Src_TrainerEX:
                    {
                        switch (idx)
                        {
                            case 1: // Shauna in Aquacorde Town
                                isEvtSet = eventWorkHelper.GetWork(0x074) >= 3;
                                break;

                            case 2: // Prof. Sycamore for a Kanto partner
                                isEvtSet = eventWorkHelper.GetWork(0x0D5) >= 3;
                                break;

                            case 3: // Tierno in Route 5
                                isEvtSet = eventWorkHelper.GetWork(0x07D) >= 2;
                                break;

                            case 4: // Multi-battle against Trevor and Tierno in Route 7
                                isEvtSet = eventWorkHelper.GetWork(0x07F) >= 13;
                                break;

                            case 5: // Team Flare Grunt 1 in Glittering Cave
                                isEvtSet = eventWorkHelper.GetWork(0x088) >= 2;
                                break;

                            case 6: // Multi-Battle Team Flare Grunts in Glittering Cave
                                isEvtSet = eventWorkHelper.GetWork(0x088) >= 4;
                                break;

                            case 7: // Team Flare Grunt 1 in Route 10
                                isEvtSet = eventWorkHelper.GetWork(0x0A0) >= 2;
                                break;

                            case 8: // Korrina in Geosenge Town
                                isEvtSet = eventWorkHelper.GetWork(0x09F) >= 4;
                                break;

                            case 9: // Calem/Serena in Tower of Mastery
                                isEvtSet = eventWorkHelper.GetWork(0x09E) >= 7;
                                break;

                            case 10: // Korrina in Tower of Mastery
                                isEvtSet = eventWorkHelper.GetWork(0x098) >= 2;
                                break;

                            default:
                                isEvtSet = false;
                                break;
                        }
                    }
                    break;

                case Src_GiftEX:
                    {
                        switch (idx)
                        {
                            case 1: // Town map from Mom
                                isEvtSet = eventWorkHelper.GetWork(0x07C) >= 7;
                                break;

                            case 2: // Poké Balls gift in Route 2
                                isEvtSet = eventWorkHelper.GetWork(0x075) == 2;
                                break;

                            case 3: // Paralyze Heal gift in Santalune Forest
                                isEvtSet = eventWorkHelper.GetWork(0x077) >= 3;
                                break;

                            case 4: // Adventure Rules gift in Route 3
                                isEvtSet = eventWorkHelper.GetWork(0x078) >= 2;
                                break;

                            case 5: // Exp. Share from Alexa
                                isEvtSet = eventWorkHelper.GetWork(0x093) == 1;
                                break;

                            case 6: // TM27 (Return) from Dexio
                                isEvtSet = eventWorkHelper.GetWork(0x09D) > 0;
                                break;

                            case 7: // Kanto partner megastone from Prof. Sycamore
                                isEvtSet = eventWorkHelper.GetWork(0x03B) != 0;
                                break;

                            case 8: // Has Poké Radar
                                isEvtSet = HasItemInBag(savHelper.Inventory, 0x01AF);
                                break;

                            case 9: // O-Powers from Mr. Bonding at Lumiose Gate Route 5
                                isEvtSet = eventWorkHelper.GetWork(0x0D3) > 0;
                                break;

                            case 10: // Honey from Trevor in Route 5
                                isEvtSet = eventWorkHelper.GetWork(0x07D) >= 2;
                                break;

                            case 11: // Item gifts in Berry Fields
                                isEvtSet = eventWorkHelper.GetWork(0x0E1) >= 1;
                                break;

                            case 12: // TM47 (Protect) from Buttler in Parfum Palace
                                isEvtSet = eventWorkHelper.GetWork(0x07F) >= 3;
                                break;

                            case 13: // Has Old Rod
                                isEvtSet = HasItemInBag(savHelper.Inventory, 0x01BD);
                                break;

                            case 14: // Fossil in Glittering Cave
                                isEvtSet = eventWorkHelper.GetWork(0x088) >= 5;
                                break;

                            case 15: // Dowsing Machine in Route 8
                                isEvtSet = eventWorkHelper.GetWork(0x08B) >= 3;
                                break;

                            case 16: // Bicycle in Cyllage City
                                isEvtSet = eventWorkHelper.GetWork(0x08A) >= 1;
                                break;

                            case 17: // HM04 (Strength) in Cyllage City
                                isEvtSet = eventWorkHelper.GetWork(0x089) >= 1;
                                break;

                            case 18: // TM70 (Flash) in Reflection Cave
                                isEvtSet = eventWorkHelper.GetWork(0x0DE) >= 1;
                                break;

                            case 19: // Intriguing Stone in Shalour City
                                isEvtSet = eventWorkHelper.GetWork(0x097) >= 2;
                                break;

                            case 20: // HM03 (Surf) in Shalour City
                                isEvtSet = eventWorkHelper.GetWork(0x097) == 8;
                                break;

                            case 21: // Mega Ring in Tower of Mastery
                                isEvtSet = eventWorkHelper.GetWork(0x098) >= 2;
                                break;
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

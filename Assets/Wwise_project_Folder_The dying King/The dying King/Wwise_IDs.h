/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BUTTON_BACKTOMENU = 3250874435U;
        static const AkUniqueID BUTTON_MOUSECLICK = 2312127631U;
        static const AkUniqueID BUTTON_MOUSEOVER = 673585283U;
        static const AkUniqueID GAMESTARTUP = 3161578462U;
        static const AkUniqueID KINGDEATH_EARLY = 1902257150U;
        static const AkUniqueID KINGDEATH_REGULAR = 4013156071U;
        static const AkUniqueID PLAYBUTTON_CLICK = 3852047270U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace KINGSTATE
        {
            static const AkUniqueID GROUP = 3115119195U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD_EARLY = 493873435U;
                static const AkUniqueID DEAD_REGULAR = 1697941970U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace KINGSTATE

        namespace MENUSTATE
        {
            static const AkUniqueID GROUP = 1548586727U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID INMENU = 3374585465U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID STARTUP = 2530218128U;
            } // namespace STATE
        } // namespace MENUSTATE

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID ANGER = 954941502U;
        static const AkUniqueID EMPATHY = 1561647163U;
        static const AkUniqueID LOYALTY_AMBITION = 3105742403U;
        static const AkUniqueID PLAYBACK_RATE = 1524500807U;
        static const AkUniqueID RPM = 796049864U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAINSOUNDBANK = 534561221U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MOTION_FACTORY_BUS = 985987111U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID DEFAULT_MOTION_DEVICE = 4230635974U;
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__

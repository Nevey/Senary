using Paladin.Framework.Enums;

namespace ApplicationManaging
{
    public partial class ApplicationState
    {
        private static ApplicationManaging.ApplicationStates values;
        private static ApplicationManaging.ApplicationState mainState;
        private static ApplicationManaging.ApplicationState gamePlayState;

        public static ApplicationManaging.ApplicationStates Values
        {
            get
            {
                if (values == null)
                    values = (ApplicationManaging.ApplicationStates)EnumRegistry.GetEnum("81d6ba6b0b2c3ee488eb1596c3ffa676");
                return values;
            }
        }

        public static ApplicationManaging.ApplicationState MainState
        {
            get
            {
                if (mainState == null && Values != null)
                    mainState = (ApplicationManaging.ApplicationState)Values.GetEnumItem("111e9ccd68d971745a25c739dbdd1dd1");
                return mainState;
            }
        }

        public static ApplicationManaging.ApplicationState GamePlayState
        {
            get
            {
                if (gamePlayState == null && Values != null)
                    gamePlayState = (ApplicationManaging.ApplicationState)Values.GetEnumItem("75d0b78d4266346498d7a1d63b4a30bf");
                return gamePlayState;
            }
        }

    }
}

